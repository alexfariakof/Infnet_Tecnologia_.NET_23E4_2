using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates.Strategy
{
    public interface IAccountCreationStrategy
    {
        void CreateAccount(AbstractAccount account, string name, Login login, Flat flat, Card card);
    }
}
