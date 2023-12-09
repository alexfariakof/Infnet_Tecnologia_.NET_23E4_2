using Domain.Account.Agreggates;
using Domain.Core.Aggreggates;

namespace Domain.Account.Agreggates
{
    public class PlaylistPersonal : PlayListBase
    {
        public Customer Customer { get; set; }
        public bool Public { get; set; }
        public DateTime DtCreated { get; set; }
    }
}
