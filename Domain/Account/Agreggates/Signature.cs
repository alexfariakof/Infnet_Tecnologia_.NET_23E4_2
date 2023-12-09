using Domain.Core.AggreggatesBase;
using Domain.Streaming.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Signature : Base
    {
        public Flat Flat { get; set; }
        public Boolean Active { get; set; }
        public DateTime DtActivation{ get; }
    }
}
