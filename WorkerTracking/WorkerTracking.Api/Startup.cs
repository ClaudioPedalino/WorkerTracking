///using HealthChecks.UI.Client;
///using Newtonsoft.Json;
///using Newtonsoft.Json.Linq;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Collections.Generic;
using System.Text;
using WorkerTracking.Api.Auth;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Handlers;
using WorkerTracking.Data;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Data.Repositories;
using WorkerTracking.Entities;

namespace WorkerTracking.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy(name: "WorkerApiCors", options =>
                    options
                        .WithOrigins("http://localhost:4200", "http://localhost:4200/dashboard")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRoleCommand>());

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<DataContext>();

            RegisterDatabase(services);

            services.AddScoped<IIdentityService, IdentityService>();
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                        ///IssuerSigningKeyValidator = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings::Secret"]))
                    };
                });

            services.AddMediatR(typeof(GetAllWorkersQueryHandler).Assembly);
            RegisterRepositories(services);
            RegisterLogging(services);
            RegisterSwagger(services);



            #region TODO: fix health check
            //services.AddHealthChecks()
            //   .AddNpgSql(Configuration.GetConnectionString("PostgreSql"),
            //       failureStatus: HealthStatus.Unhealthy,
            //       tags: new[] { "ready" });

            //services.AddHealthChecksUI().AddInMemoryStorage();
            #endregion

        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IWorkerRepository, WorkerRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IWorkersByTeamRepository, WorkersByTeamRepository>();
        }

        private void RegisterLogging(IServiceCollection services)
        {
            services.AddSingleton<Serilog.ILogger>(opt =>
            {
                return new LoggerConfiguration().WriteTo.
                    PostgreSQL(Configuration["ConnectionStrings:PostgreSql"],
                                Configuration["ConnectionStrings:LogTable"],
                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
                                needAutoCreateTable: true)
                    .CreateLogger();
            });
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Worker Tracking Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            });
        }

        private void RegisterDatabase(IServiceCollection services)
        {
            if (UsingPostgre())
                services.AddDbContext<DataContext>(options => options
                    .UseNpgsql(Configuration.GetConnectionString("PostgreSql")));
            if (UsingLocalDb())
                services.AddDbContext<DataContext>(options => options
                    .UseInMemoryDatabase(databaseName: "LocalDb"));
        }

        private bool UsingLocalDb()
            => Configuration.GetSection("DataProvider:UsingLocalDb").Value.ToString().ToLower()
                .Equals(bool.TrueString.ToLower());

        private bool UsingPostgre()
            => Configuration.GetSection("DataProvider:UsingPostgre").Value.ToString().ToLower()
                .Equals(bool.TrueString.ToLower());


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("WorkerApiCors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapHealthChecks("api/health/ready", new HealthCheckOptions()
            //    {
            //        ResultStatusCodes =
            //        {
            //            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            //            [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
            //            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            //        },
            //        ResponseWriter = WriteHealthCheckReadyResponse,
            //        Predicate = (check) => check.Tags.Contains("ready"),
            //        AllowCachingResponses = false
            //    });
            //    endpoints.MapHealthChecks("api/health/live", new HealthCheckOptions()
            //    {
            //        Predicate = (check) => !check.Tags.Contains("ready"),
            //        ResponseWriter = WriteHealthCheckLiveResponse,
            //        AllowCachingResponses = false
            //    });
            //    endpoints.MapHealthChecks("api/health/ui", new HealthCheckOptions()
            //    {
            //        Predicate = _ => true,
            //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //    });
            //});

            //app.UseHealthChecksUI();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Worker Tracking Api");
            });
        }

        //Enrich Health Check Detail
        //private Task WriteHealthCheckLiveResponse(HttpContext httpContext, HealthReport result)
        //{
        //    httpContext.Response.ContentType = "application/json";

        //    var json = new JObject(
        //            new JProperty("OverallStatus", result.Status.ToString()),
        //            new JProperty("TotalCheckDuration", result.TotalDuration.TotalSeconds.ToString("0:0.00"))
        //        );
        //    return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        //}

        //private Task WriteHealthCheckReadyResponse(HttpContext httpContext, HealthReport result)
        //{
        //    httpContext.Response.ContentType = "application/json";

        //    var json = new JObject(
        //            new JProperty("OverallStatus", result.Status.ToString()),
        //            new JProperty("TotalCheckDuration", result.TotalDuration.TotalSeconds.ToString("0:0.00")),
        //            new JProperty("DependencyHealthChecks", new JObject(result.Entries.Select(dicItem =>
        //                new JProperty(dicItem.Key, new JObject(
        //                    new JProperty("Status", dicItem.Value.Status.ToString()),
        //                    new JProperty("Duration", dicItem.Value.Duration.TotalSeconds.ToString("0:0.00")),
        //                    new JProperty("Exception", dicItem.Value.Exception?.Message),
        //                    new JProperty("Data", new JObject(dicItem.Value.Data.Select(dicData =>
        //                        new JProperty(dicData.Key, dicData.Value))))
        //                ))))
        //        ));
        //    return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        //}
    }
}
