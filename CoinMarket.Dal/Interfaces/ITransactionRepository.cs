using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMarket.Model.Entities;
using CoinMarket.Model.Models;

namespace CoinMarket.Dal.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task Buy(long customerId, decimal price, decimal amount, int coinCode);

        Task<List<Transaction>> ShowPortfolio(long customerId, int coinCode);
    }
}