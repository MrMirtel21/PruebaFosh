using Sat.Recruitment.Domain.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Context
{
    public class Context : IContext
    {
        private readonly string _userpath;
        private readonly Dictionary<string, Delegate> _tableOperations;

        public Context() 
        {
            var pathDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _userpath = Path.Combine(pathDirectory,"Context" ,"Users.txt");

            _tableOperations = new Dictionary<string, Delegate>();
            _tableOperations[nameof(User)]=new Func<User,Task>(WriteUserInUserFile);
        }

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
            List<User> users = new List<User>();

            FileStream userStream = new FileStream(_userpath, FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(userStream);
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var attributes = line.Split(',');
                string name = attributes[0];
                var user = new User()
                {
                    Name = attributes[0],
                    Email = attributes[1],
                    Phone = attributes[2],
                    Address = attributes[3],
                    Type = UserType.FromName(attributes[4]),                   
                };
                user.SetMoney(decimal.Parse(attributes[5]));

                users.Add(user);
            }
            reader.Close();
            return users;
        }
        private async Task WriteUserInUserFile(User user)
        {
            var userStream = new FileStream(_userpath, FileMode.Append, FileAccess.Write);
            await using (var writer = new StreamWriter(userStream))
            {
                await writer.WriteLineAsync(user.ToString());
            }
        }
    }
}
