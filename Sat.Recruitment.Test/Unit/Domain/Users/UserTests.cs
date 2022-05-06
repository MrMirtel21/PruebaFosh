using System;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Domain.Users;
using Sat.Recruitment.Test.Common;
using Xunit;

namespace Sat.Recruitment.Test.Unit.Domain.Users
{
    public class UserTests
    {

        public UserTests()
        {
            FakeData.InitializeUsers();
        }

        [Fact]
        public void User_AddMoney_Success()
        {
            var previousUsrMoney =  FakeData.user1.Money;
            FakeData.user1.SetMoney(previousUsrMoney+500);
            Assert.NotEqual(previousUsrMoney, FakeData.user1.Money);
        }
    }
}
