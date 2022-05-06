using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Users.MoneyManagement.RewardCalculator
{
    public interface IUserTypeRewardCalculatorFactory
    {
        IUserTypeRewardCalculator GetUserTypeRewardCalculator(UserType type);
    }

    public class UserTypeRewardCalculatorFactory : IUserTypeRewardCalculatorFactory
    {
        private readonly Dictionary<string, IUserTypeRewardCalculator> UserTypeRewardCalculators = new Dictionary<string, IUserTypeRewardCalculator> 
        {
            {UserType.Normal.Id, new NormalUserRewardCalculator() },
            {UserType.SuperUser.Id, new SuperUserRewardCalculator() },
            {UserType.Premium.Id, new PremiumUserRewardCalculator() }
        };

        public IUserTypeRewardCalculator GetUserTypeRewardCalculator(UserType type)
        {
            if (!UserTypeRewardCalculators.ContainsKey(type.Id))
            {
                throw new Exception("ProfileType not initiated");
            }
            return UserTypeRewardCalculators[type.Id];
        }
    }
}
