using System.Collections.Generic;

namespace CoinMarket.Model.Entities
{
    public class Customer:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual ICollection<ApprovedTransaction> ApprovedTransactions { get; set; }
    }
}