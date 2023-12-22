using Domain.Account.Agreggates.Strategy;
using Domain.Account.ValueObject;
using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Notifications;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Domain.Transactions.ValueObject;

namespace Domain.Account.Agreggates
{
    public abstract class AbstractAccount : BaseModel
    {
        public string Name { get; set; }
        public Login Login { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Signature> Signatures { get; set; } = new List<Signature>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public void AddCard(Card card) => this.Cards.Add(card);       

        public void AddFlat(Flat flat, Card card)
        {
            IsValidCreditCard(card.Number);
            var merchant = new Transactions.ValueObject.Merchant() { Name = flat.Name };
            card.CreateTransaction(merchant, new Monetary(flat.Value), flat.Description);
            DisableActiveSigniture();
            this.Signatures.Add(new Signature()
            {
                Active = true,
                Flat = flat,
                DtActivation = DateTime.Now,
            });
        }

        private IAccountCreationStrategy accountCreationStrategy;
        public void SetAccountCreationStrategy(IAccountCreationStrategy strategy)
        {
            accountCreationStrategy = strategy;
        }

        public void CreateAccount(string name, Login login, Flat flat, Card card)
        {
            accountCreationStrategy?.CreateAccount(this, name, login, flat, card);
        }

        private void DisableActiveSigniture()
        {
            if (this.Signatures.Count > 0 && this.Signatures.Any(x => x.Active))
                this.Signatures.FirstOrDefault(x => x.Active).Active = false;
        }

        private static void IsValidCreditCard(string creditCardNumber)
        {
            var cardInfo = CreditCardBrandInfo.IdentifyCard(creditCardNumber);

            if (!cardInfo.IsValid)
                throw new ArgumentException($"Cartão { cardInfo.Name } inválido.");
            else if (cardInfo.Brand == CreditCardBrandInfo.CreditCardBrand.Invalid)
                throw new ArgumentException($"Cartão { cardInfo.Name }.");                

        }
    }
}
