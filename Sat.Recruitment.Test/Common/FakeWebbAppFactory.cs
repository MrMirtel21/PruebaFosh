using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using Sat.Recruitment.Test.Common.Web;

namespace Sat.Recruitment.Test.Common
{
    public class FakeWebbAppFactory : TestWebApplicationFactory<Startup, FakeContext>
    {
        protected override async Task ConfigureTestServices(IServiceCollection services)
        {
            await Task.FromResult(true);
        }
    }
}
