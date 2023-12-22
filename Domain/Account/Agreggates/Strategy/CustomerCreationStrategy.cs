using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates.Strategy
{
    public class CustomerCreationStrategy : IAccountCreationStrategy
    {
        public void CreateAccount(AbstractAccount account, string name, Login login, Flat flat, Card card)
        {
            var customer = (Customer)account;
            customer.Name = name;
            customer.Login = login;
            customer.AddFlat(flat, card);
            customer.AddCard(card);
        }
    }
}