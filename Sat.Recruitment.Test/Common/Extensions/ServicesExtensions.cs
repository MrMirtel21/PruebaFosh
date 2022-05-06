using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Infrastructure.Context;

namespace Sat.Recruitment.Test.Common.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddFakeDbContext(this IServiceCollection services)
        {
            return services.AddSingleton<IContext, FakeContext>();
        }
    }
}
