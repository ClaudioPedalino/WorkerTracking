using EnumsNET;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Collections.Generic;
using System.Linq;
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

            services.AddMediatR(typeof(GetAllWorkerersQueryHandler).Assembly);

            services.AddTransient<IWorkerRepository, WorkerRepository>();

            services.AddSingleton<Serilog.ILogger>(opt =>
            {
                return new LoggerConfiguration().WriteTo.
                    PostgreSQL(Configuration["ConnectionStrings:PostgreSql"],
                                Configuration["ConnectionStrings:LogTable"],
                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
                                needAutoCreateTable: true)
                    .CreateLogger();
            });

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Worker Tracking Api");
            });
        }
    }
}
