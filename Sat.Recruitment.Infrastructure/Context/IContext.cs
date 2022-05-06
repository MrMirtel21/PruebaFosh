using Sat.Recruitment.Domain.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Context
{
    public interface IContext
    {
        Task<List<User>> Users { get; }
        Task Add(object entity);
    }
}
