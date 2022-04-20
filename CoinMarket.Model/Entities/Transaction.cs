using System.ComponentModel.DataAnnotations.Schema;

namespace CoinMarket.Model.Entities
{
    public class Transaction : BaseEntity
    {
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Coin")]
        public long CoinId { get; set; }
        public Coin Coin { get; set; }
        public Customer Customer { get; set; }
    }
}
