using System;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Domain.Users;

namespace Sat.Recruitment.Test.Common
{
    public class FakeData
    {
        public static User user1 { get; set; }

        public static void InitializeUsers()
        {
            user1= new User
            {
                Name = "Text",
                Address = "Text",
                Email = "Text",
                Phone = "Text",
                Type = UserType.Normal
            };
            user1.SetMoney(200);
        }
    }
}
