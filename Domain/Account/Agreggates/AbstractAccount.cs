using Domain.Account.ValueObject;
using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Notifications;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Account.Agreggates
{
    public abstract class AbstractAccount : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Signature> Signatures { get; set; } = new List<Signature>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public void AddCard(Card card) => this.Cards.Add(card);
        public void AddFlat(Flat flat, Card card)
        {
            this.IsValidCreditCard(card.Number);
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
        protected void DisableActiveSigniture()
        {
            if (this.Signatures.Count > 0 && this.Signatures.Any(x => x.Active))
                this.Signatures.FirstOrDefault(x => x.Active).Active = false;
        }

        public String CryptoPasswrod(string openPassword)
        {
            SHA256 criptoProvider = SHA256.Create();

            byte[] btexto = Encoding.UTF8.GetBytes(openPassword);

            var criptoResult = criptoProvider.ComputeHash(btexto);

            return Convert.ToHexString(criptoResult);
        }

        private void IsValidCreditCard(string creditCardNumber)
        {
            var cardInfo = CreditCardBrandInfo.IdentifyCard(creditCardNumber);

            if (cardInfo.IsValid == false)
                throw new Exception($"Cartão { cardInfo.Name } inválido.");
            else if (cardInfo.Brand == CreditCardBrandInfo.CreditCardBrand.Invalid)
                throw new Exception($"Cartão { cardInfo.Name }.");                

        }

    }
}
