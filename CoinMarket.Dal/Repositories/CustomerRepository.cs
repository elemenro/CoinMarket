using System.Linq;
using System.Threading.Tasks;
using CoinMarket.Dal.Interfaces;
using CoinMarket.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Dal.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly MyContext _context;
        public CustomerRepository(MyContext context) : base(context)
        {
            _context = context;
        }

    }
}