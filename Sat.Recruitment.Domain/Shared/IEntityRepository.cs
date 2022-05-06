using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Shared
{
    public interface IEntityRepository<T> where T: EntityBase
    {
        Task Create(T entity);
        Task<List<T>> GetAll();
    }
}
