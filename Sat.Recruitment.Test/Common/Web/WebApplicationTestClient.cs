using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Sat.Recruitment.Infrastructure.Context;
using Xunit;

namespace Sat.Recruitment.Test.Common.Web
{
    public class WebApplicationTestClient<TStartup, TDbContext> : IClassFixture<TestWebApplicationFactory<TStartup, TDbContext>>
        where TStartup : class
        where TDbContext : IContext
    {
        public HttpClient Client { get; }

        public WebApplicationTestClient(TestWebApplicationFactory<TStartup, TDbContext> fixture)
        {
            Client = fixture.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
