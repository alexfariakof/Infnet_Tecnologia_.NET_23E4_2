using Domain.Core.AggreggatesBase;
using Domain.Core.ValueObject;

namespace Domain.Streaming.Agreggates
{
    public class Flat : Base
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public Monetary Value { get; set; }
    }
}
