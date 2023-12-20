using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Notifications;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Account.Agreggates
{
    public abstract class AbstractAccount : BaseModel, IAbstractAccount
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
    }

    public interface IAbstractAccount
    {

        public void AddCard(Card card);
        public void AddFlat(Flat flat, Card card);
        public String CryptoPasswrod(string openPassword);
    }
}
