using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMarket.Model.Entities;

namespace CoinMarket.Dal.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> Get(params object[] args);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}