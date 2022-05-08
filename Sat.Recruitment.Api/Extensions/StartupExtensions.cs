using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.Domain.Users;
using Sat.Recruitment.Domain.Users.MoneyManagement.RewardCalculator;
using Sat.Recruitment.Infrastructure;
using Sat.Recruitment.Infrastructure.Context;

namespace Sat.Recruitment.Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services) 
        {
            return services.AddScoped<IContext, Context>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            return services.AddTransient<IUserRepository, UserRepository>()
                           .AddTransient<IUserMoneyManager, UserMoneyManager>()
                           .AddTransient<IUserTypeRewardCalculatorFactory, UserTypeRewardCalculatorFactory>();
        }
    }
}
