using Sat.Recruitment.Domain.Users;
using Sat.Recruitment.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        protected readonly IContext Context;

        public UserRepository(IContext context)
        {
            Context = context;
        }

        public async Task Create(User entity)
        {
            await Context.Add(entity);
        }

        public async Task<List<User>> GetAll()
        {
            return await Context.Users;
        }
    }
}
