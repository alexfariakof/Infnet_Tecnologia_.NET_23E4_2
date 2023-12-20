using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Customer : AbstractAccount, IAbstractAccount
    {
        private const string PLAYLIST_NAME = "Favoritas";
        public string CPF { get; set; }
        public DateTime Birth { get; set; }
        public List<PlaylistPersonal> Playlists { get; set; } = new List<PlaylistPersonal>();

        public void CreateAccount(string nome, string email, string password, string cpf,  DateTime birth, Flat flat, Card card)
        {
            this.Name = nome;
            this.Email = email;
            this.Birth = birth;
            this.CPF = cpf;
            this.Password = this.CryptoPasswrod(password);
            this.AddFlat(flat, card);
            this.AddCard(card);
            this.CreatePlaylist(name: PLAYLIST_NAME, @public: false);
        }

        public void CreatePlaylist(string name, bool @public = true)
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
