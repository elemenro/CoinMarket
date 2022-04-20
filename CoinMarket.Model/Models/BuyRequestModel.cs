namespace CoinMarket.Model.Models
{
    public class BuyRequestModel
    {
        public long CustomerId { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public int CoinCode { get; set; }
    }
}