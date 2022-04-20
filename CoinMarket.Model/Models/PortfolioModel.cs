namespace CoinMarket.Model.Models
{
    public class PortfolioModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Balance { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string NetWorth { get; set; }

        public string Last24HStatus { get; set; }
        public string AvgBuyPrice { get; set; }
        public string ProfitOrLoss { get; set; }
        public string PercantageOfProfitOrLoss { get; set; }
    }
}