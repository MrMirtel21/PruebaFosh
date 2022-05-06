using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Sat.Recruitment.Infrastructure.Context;

namespace Sat.Recruitment.Test.Common.Web
{
    public abstract class TestWebApplicationFactory<TStartup, TDbContext> : WebApplicationFactory<TStartup>
            where TStartup : class
            where TDbContext : IContext
    {
        private WebHostTestConfigurator<TStartup, TDbContext> _configurator;

        private WebApplicationTestClient<TStartup, TDbContext> _client;

        private WebApplicationTestClient<TStartup, TDbContext> Client
        {
            get { return _client ??= new WebApplicationTestClient<TStartup, TDbContext>(this); }
        }

        public HttpClient HttpClient => Client.Client;

        public IConfiguration Configuration => Client != null
            ? _configurator.Configuration
            : null;

        public IServiceScopeFactory Factory => Client != null
            ? _configurator.ServiceProvider.GetRequiredService<IServiceScopeFactory>()
            : null;


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _configurator = new WebHostTestConfigurator<TStartup, TDbContext>
            {
                ConfigureTestServices = ConfigureTestServices,
            };
            _configurator.ConfigureWebHost(builder);
        }

        protected abstract Task ConfigureTestServices(IServiceCollection services);

        public HttpContent ConvertToHttpContent<T>(T dto)
        {
            string payload = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            return content;
        }

        public async Task<T> ConvertToEntity<T>(HttpContent content)
        {
            return JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
        }

        public async Task RunScoped(Func<IServiceScope, Task> task)
        {
            using (var scope = Factory.CreateScope())
            {
                await task(scope);
            }
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x => { x.UseTestServer().UseStartup<TStartup>(); });
            return builder;
        }
    }
}
