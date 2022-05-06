using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Users.MoneyManagement.RewardCalculator
{
    public interface IUserTypeRewardCalculator
    {
        decimal ApplyReward(decimal money);
    }

    public class NormalUserRewardCalculator : IUserTypeRewardCalculator
    {
        private const decimal MoreThan100Multiplier = 1.12M;
        private const decimal LeesThan10Multiplier = 1.8M;
        public decimal ApplyReward(decimal money)
        {
            return (money > 100) ? money*MoreThan100Multiplier : 
                    (money < 10)? money*LeesThan10Multiplier: 
                    money;
        }
    }
    public class SuperUserRewardCalculator : IUserTypeRewardCalculator
    {
        private const decimal MoreThan100Multiplier = 1.2M;
        public decimal ApplyReward(decimal money)
        {
            return (money > 100) ? money * MoreThan100Multiplier :
                    money;
        }
    }
    public class PremiumUserRewardCalculator : IUserTypeRewardCalculator
    {
        private const decimal MoreThan100Multiplier = 3;
        public decimal ApplyReward(decimal money)
        {
            return (money > 100) ? money * MoreThan100Multiplier:
                    money;
        }
    }
}
