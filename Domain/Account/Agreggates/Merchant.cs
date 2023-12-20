using Domain.Core.Interfaces;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Merchant : Account, IMerchant
    {
        public string CNPJ { get; set; }

        public void CreateAccount(string name, string email, string password, string cnpj, Flat flat, Card card)
        {
            this.Name = name;
            this.Email = email;
            this.CNPJ = cnpj;
            this.Password = this.CryptoPasswrod(password);
            this.AddFlat(flat, card);
            this.AddCard(card);
        }
    }
}
