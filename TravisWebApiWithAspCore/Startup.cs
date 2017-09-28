using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using TravisWebApiWithAspCore.Services;
using TravisWebApiWithAspCore.Entities;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System.Runtime.InteropServices;
using TravisWebApiWithAspCore.Models;

namespace TravisWebApiWithAspCore
{
    public class Startup
    {
        private bool isWindows = false;
        private bool isMac = false;

		public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            isMac = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }

        public static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(o =>
                {
                    if (o.SerializerSettings.ContractResolver != null)
                    {
                        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                        castedResolver.NamingStrategy = null;
                    }
                });

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();

#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

			if(isWindows == true)
            {
				var sqlConnectionString = Configuration["MySqlConnectionStrings:DataAccessMySqlProviderWindow"];

				services.AddDbContext<CityInfoContext>(options =>
					options.UseMySQL(sqlConnectionString)
				);
            }

            if(isMac == true)
            {
				var sqlConnectionString = Configuration["MySqlConnectionStrings:DataAccessMySqlProviderMac"];

				services.AddDbContext<CityInfoContext>(options =>
					options.UseMySQL(sqlConnectionString)
				);
            }

            services.AddScoped<ICityInfoRepository,CityInfoRespository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            CityInfoContext cityInfoContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            loggerFactory.AddNLog();
            loggerFactory.ConfigureNLog("nlog.config");

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            cityInfoContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(cfg=>
            {
                cfg.CreateMap<City,CityWithoutPointOfInterestDto>();
                cfg.CreateMap<City, CityDto>();
                cfg.CreateMap<PointOfInterest, PointsOfInterestDto>();
                cfg.CreateMap<PointOfInterestCreationDto, PointOfInterest>();
                cfg.CreateMap<PointOfInterestUpdateDto, PointOfInterest>();
                cfg.CreateMap<PointOfInterest, PointOfInterestUpdateDto>();
            });
      
            app.UseStatusCodePages();

            app.UseMvc();
        }
    }
}
