using Domain.Core.AggreggatesBase;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Customer : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string CPF { get; set; }
        public DateTime Birth { get; set; }
        public List<Card> Cards{ get; set; } = new List<Card>();
        public List<Signature> Signatures{ get; set; } = new List<Signature>();
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

    }
}
