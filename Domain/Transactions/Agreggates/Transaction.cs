using Domain.Core.AggreggatesBase;
using Domain.Core.ValueObject;
using Domain.Transactions.ValueObject;

namespace Domain.Transactions.Agreggates
{
    public class Transaction : Base
    {
        public DateTime DtTransaction { get; set; }
        public Monetary Value { get; set; }
        public String Description { get; set; }
        public Merchant Merchant { get; set; }        

    }
}
