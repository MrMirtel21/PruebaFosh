using Sat.Recruitment.Domain.Users.MoneyManagement.RewardCalculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Users
{
    public interface IUserMoneyManager 
    {
        public decimal ApplyReward(decimal money, UserType type);
    }
    public class UserMoneyManager : IUserMoneyManager
    {
        private readonly IUserTypeRewardCalculatorFactory _userTypeFactory;

        public UserMoneyManager(IUserTypeRewardCalculatorFactory userTypeFactory)
        {
            _userTypeFactory = userTypeFactory;
        }

        public decimal ApplyReward(decimal money, UserType type) 
        {
            var userTypeRewardCalculator = _userTypeFactory.GetUserTypeRewardCalculator(type);
            var moneyAfterReward = userTypeRewardCalculator.ApplyReward(money);
            return moneyAfterReward;
        }
    }
}
