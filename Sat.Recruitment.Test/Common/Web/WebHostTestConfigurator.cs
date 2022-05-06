using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Infrastructure.Context;
using Sat.Recruitment.Test.Common.Extensions;


namespace Sat.Recruitment.Test.Common.Web
{
    public class WebHostTestConfigurator<TStartup,TContext>
        where TStartup : class
        where TContext : IContext
    {
        private IConfiguration _configuration;
        public IConfiguration Configuration => _configuration;
        private IServiceProvider _serviceProvider;
        public IServiceProvider ServiceProvider => _serviceProvider;
        private static IConfigurationBuilder ConfigureTestAppSettings(IConfigurationBuilder config)
        {
            var jsonSources = config.Sources.Where(x => x is JsonConfigurationSource).ToList();
            foreach (var jsonSource in jsonSources)
            {
                config.Sources.Remove(jsonSource);
            }

            var basePath = Directory.GetCurrentDirectory();
            config.SetBasePath(basePath);
            config.AddJsonFile("appsettings.json", true);
            config.AddJsonFile("appsettings.development.json", true);

            return config;
        }
        public void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config => { _configuration = ConfigureTestAppSettings(config).Build(); });

            // will be called after the `ConfigureServices` from the Startup
            builder.ConfigureTestServices(services =>
            {
                services.AddFakeDbContext();

                ConfigureTestServices(services).Wait();
                var sp = services.BuildServiceProvider();

                _serviceProvider = sp;
            });
        }

        public Func<IServiceCollection, Task> ConfigureTestServices { get; set; }
    }
}
