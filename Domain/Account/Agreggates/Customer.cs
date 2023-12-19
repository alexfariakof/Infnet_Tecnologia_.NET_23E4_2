using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Customer : Account
    {
        private const string PLAYLIST_NAME = "Favoritas";

        public string CPF { get; set; }
        public DateTime Birth { get; set; }
        public List<PlaylistPersonal> Playlists { get; set; } = new List<PlaylistPersonal>();

        public void CreateAccount(string nome, string email, string senha, string cpf,  DateTime dtNascimento, Flat flat, Card card)
        {
            this.Name = nome;
            this.Email = email;
            this.Birth = dtNascimento;
            this.CPF = cpf;

            //Criptografar a senha
            this.Password = this.CryptoPasswrod(senha);

            //Assinar um plano
            this.AddFlat(flat, card);

            //Adicionar cartão na conta do usuário
            this.AddCard(card);

            //Criar a playlist padrão do usuario
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
