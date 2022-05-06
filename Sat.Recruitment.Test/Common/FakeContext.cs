using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sat.Recruitment.Domain.Users;
using Sat.Recruitment.Infrastructure.Context;

namespace Sat.Recruitment.Test.Common
{
    public class FakeContext : IContext
    {
        private readonly Dictionary<string, Delegate> _tableOperations;

        public FakeContext()
        {
            var user1 = new User
            {
                Name = "Paco",
                Email = "Paco@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1367354215",
                Type = UserType.Premium,
            };
            user1.SetMoney(600);
            _users = new List<User>
            {
                user1
            };
            _tableOperations = new Dictionary<string, Delegate>();
            _tableOperations[nameof(User)] = new Func<User, Task>(WriteUserInUserFile);
        }

        public static List<User> _users;
        Task<List<User>> IContext.Users => ReadUserFile();

        public async Task Add(object entity)
        {
            var entityType = entity.GetType().Name;
            if (_tableOperations.ContainsKey(entityType))
            {
                await (_tableOperations[entityType].DynamicInvoke((User)entity) as Task);
            }
        }

        private async Task<List<User>> ReadUserFile()
        {
            await Task.FromResult(true);
            return _users;
        }
        private async Task WriteUserInUserFile(User user)
        {
            await Task.FromResult(true);
            _users.Add(user);
        }
    }
}
