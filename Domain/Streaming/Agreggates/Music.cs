using Domain.Core.AggreggatesBase;
using Domain.Transactions.ValueObject;

namespace Domain.Streaming.Agreggates
{
    public class Music : Base
    {
        public String Name { get; set; }
        public Merchant Duration { get; set; }
    }
}
