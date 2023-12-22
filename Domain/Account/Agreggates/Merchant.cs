using Domain.Account.ValueObject;
using Domain.Core.Interfaces;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Merchant : AbstractAccount, IMerchant
    {
        public string CNPJ { get; set; }

        public void CreateAccount(string name, Login login, string cnpj, Flat flat, Card card)
        {
            Name = name;
            Login = login;
            CNPJ = cnpj;            
            AddFlat(flat, card);
            AddCard(card);
        }
    }
}
