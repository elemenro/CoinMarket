using System;
using System.Linq;
using CoinMarket.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoinMarket.Dal
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MyContext(serviceProvider.GetRequiredService<DbContextOptions<MyContext>>());
            
            context.Customers.Add(new Customer
            {
                FirstName = "Utku",
                LastName = "YIldız",
                Balance = 10000
            });

            context.Coins.Add(new Coin {Code = "BNB", Name = "BNB"});
            context.SaveChanges();
        }
    }
}