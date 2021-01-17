using System;
using AutoMapper;
using LaundryIroningData.DataContext;
using LaundryIroningExceptionHandling;
using LaundryIroningHelper;
using LaundryIroningLogging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LaundryIroningAPI
{
    public class Startup
    {
        readonly IWebHostEnvironment HostingEnvironment;
        public IConfigurationRoot Configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            HostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();


            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
           

            string str = Configuration.GetConnectionString("SQLApiConnection");

            switch (Configuration["Settings:UseDatabase"])
            {
                //Remove case and disable mysql code
                case "SqlDatabase":
                    //Add SQl connection string settings
                    services.AddDbContext<ApiDBContext>(c =>
                     c.UseSqlServer(str,
                     sqlServerOptionsAction: sqlOptions =>
                     {
                         sqlOptions.EnableRetryOnFailure(
                             maxRetryCount: Convert.ToInt32(Configuration["Settings:SqlServerMaxRetryCount"]),
                             maxRetryDelay: TimeSpan.FromSeconds(Convert.ToDouble(Configuration["Settings:SqlServerMaxRetryDelay"])),
                             errorNumbersToAdd: null
                         );
                         sqlOptions.CommandTimeout(600);
                     }), ServiceLifetime.Transient);


                    Container.DIContainer.SQLContainer.Injector(services);
                    break;

            }

            services.AddResponseCompression();
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetPreflightMaxAge(TimeSpan.FromSeconds(Convert.ToDouble(Configuration["Settings:SetPreflightMaxAge"])));
            }));

            //Added versioning for API, default is 1.0
            // for performance issue
            //services.AddApiVersioning(options =>
            //{
            //    options.ReportApiVersions = true;
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    //Set API default version as 2.0
            //    options.DefaultApiVersion = new ApiVersion(2, 0);
            //    //Accept version details in request header
            //    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //});

            services.AddMvc(options =>
            {                
                options.Filters.Add(new ConsumesAttribute("application/json"));
            }).AddNewtonsoftJson(
              options =>
              {
                  options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              }
          );
            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromSeconds(15724800);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseStaticFiles();

            app.UseResponseCompression();

            app.UseCors("CorsPolicy");
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                context.Response.Headers.Add("Pragma", "no-cache");
                context.Response.Headers.Add("Expires", "-1");
                context.Response.Headers.Add("api-supported-versions", "");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Request.EnableBuffering();
                await next();
            });

            app.UseRouting();

            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            setGlobalVariables();
        }

        private void setGlobalVariables()
        {
            WriteLogFile.m_exePath = HostingEnvironment.ContentRootPath;
            WriteLogFile.m_appLogPath = Configuration.GetValue<string>("Settings:LogFilePath");
        }
    }
}
