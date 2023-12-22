using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Customer : AbstractAccount
    {
        private const string PLAYLIST_NAME = "Favoritas";
        public string CPF { get; set; }
        public DateTime Birth { get; set; }
        public List<PlaylistPersonal> Playlists { get; set; } = new List<PlaylistPersonal>();
        public void CreateAccount(string name, Login login, DateTime birth, string cpf, Flat flat, Card card)
        {
            Name = name;
            Login = login;
            Birth = birth;
            CPF = cpf;            
            AddFlat(flat, card);
            AddCard(card);
            CreatePlaylist(name: PLAYLIST_NAME, @public: false);
        }
        private void CreatePlaylist(string name, bool @public = true)
        {
            this.Playlists.Add(new PlaylistPersonal()
            {
                Name = name,
                IsPublic = @public,
                DtCreated = DateTime.Now,
                Customer = this
            });
        }        
    }
}
