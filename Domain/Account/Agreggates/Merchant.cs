using Domain.Account.Agreggates.Strategy;
using Domain.Account.ValueObject;
using Domain.Core.Interfaces;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Merchant : AbstractAccount, IMerchant
    {
        public string CNPJ { get; set; }

        public Merchant CreateAccount(string name, Login login, string cnpj, Flat flat, Card card)
        {
            var merchant = new Merchant();
            merchant.Name = name;
            merchant.Login = login;
            merchant.CNPJ = cnpj;
            merchant.SetAccountCreationStrategy(new CustomerCreationStrategy());
            merchant.CreateAccount(name, login, flat, card);            
            return merchant;
        }
    }
}
