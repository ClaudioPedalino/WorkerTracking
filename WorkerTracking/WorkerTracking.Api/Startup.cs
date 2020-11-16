using EnumsNET;
//using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerTracking.Core.Common;
using WorkerTracking.Core.Handlers;
using WorkerTracking.Data;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Data.Repositories;

namespace WorkerTracking.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            RegisterDatabase(services);

            services.AddMediatR(typeof(GetAllWorkersQueryHandler).Assembly);

            RegisterRepositories(services);
            RegisterLogging(services);
            RegisterSwagger(services);

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

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
                .Equals(BooleanEnum.True.ToString().ToLower());

        private bool UsingPostgre()
            => Configuration.GetSection("DataProvider:UsingPostgre").Value.ToString().ToLower()
                .Equals(BooleanEnum.True.ToString().ToLower());


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyOrigin());


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
