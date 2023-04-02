using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DALL.DTOs;
using Serilog;
using System.IO;

namespace Api_Midasoft
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        internal static MapperConfiguration mapperConfiguration { get; set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //AutoMapper
            mapperConfiguration = MapperConfig.mapperConfiguration();


            ///Configuracion de serilog
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
      .WriteTo.Map("Name", "Other", (name, wt) => wt.File(Path.Combine(logDirectory, $"log-{DateTimeOffset.Now:yyyy-MM-dd-HH-mm-ss}.txt")))
      .CreateLogger();

        }
        protected void Application_End()
        {
            Log.CloseAndFlush();
        }
    }
}
