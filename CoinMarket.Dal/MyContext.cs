using System;
using CoinMarket.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Dal
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<ApprovedTransaction> ApprovedTransactions { get; set; }
        
    }
}
