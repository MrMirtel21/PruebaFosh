using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Domain.Shared
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> AsQueryable { get; }
    }
}
