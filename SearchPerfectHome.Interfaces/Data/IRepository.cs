using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<IQueryable<T>> GetAll(string userId);

        Task<T> Get(int id, int profileId);

        Task<T> Get(T item);

        Task<T> Create();

        Task<bool> Add(T item);

        Task<bool> Update(T item);

        Task<bool> Delete(T item);
    }
}
