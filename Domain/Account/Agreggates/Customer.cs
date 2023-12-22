using Domain.Account.Agreggates.Interfaces;
using Domain.Account.Agreggates.Strategy;
using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Customer : AbstractAccount, ICustomer
    {
        private const string PLAYLIST_NAME = "Favoritas";
        public string CPF { get; set; }
        public DateTime Birth { get; set; }
        public List<PlaylistPersonal> Playlists { get; set; } = new List<PlaylistPersonal>();
        public Customer CreateAccount(string name, Login login, DateTime birth, string cpf,   Flat flat, Card card)
        {
            var customer = new Customer();
            customer.Name = name;
            customer.Login = login;
            customer.Birth = birth;
            customer.CPF = cpf;
            customer.SetAccountCreationStrategy(new CustomerCreationStrategy());
            customer.CreateAccount(name, login, flat, card);
            customer.CreatePlaylist(name: PLAYLIST_NAME, @public: false);
            return customer;
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
