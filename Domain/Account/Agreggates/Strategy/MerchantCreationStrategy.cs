using Domain.Account.Agreggates;
using Domain.Account.Agreggates.Strategy;
using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

public class MerchantCreationStrategy : IAccountCreationStrategy
{
    public void CreateAccount(AbstractAccount account, string name, Login login, Flat flat, Card card)
    {
        var merchant = (Merchant)account;
        merchant.Name = name;
        merchant.Login = login;
        merchant.AddFlat(flat, card);
        merchant.AddCard(card);
    }
}
