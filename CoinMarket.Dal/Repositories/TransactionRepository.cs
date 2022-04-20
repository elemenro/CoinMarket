using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMarket.Dal.Interfaces;
using CoinMarket.Model.Entities;
using CoinMarket.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Dal.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly MyContext _context;
        public TransactionRepository(MyContext context) : base(context)
        {
            _context = context;
        }
        public async Task Buy(long customerId, decimal price, decimal amount, int coinCode)
        {
            await Insert(new Transaction
            {
                CoinId = coinCode,
                Price = price,
                Quantity = amount,
                CustomerId = customerId
            });
         }


        public async Task<List<Transaction>> ShowPortfolio(long customerId, int coinCode)
        {
           return await _context.Transactions.Where(x => x.CustomerId == customerId && x.CoinId == coinCode)
                    //.Select(x => x.Price * x.Quantity)
                    .ToListAsync();

        }
    }
}