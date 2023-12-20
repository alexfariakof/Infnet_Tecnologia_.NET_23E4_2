using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Notifications;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Account.Agreggates
{
    public abstract class Account : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Signature> Signatures { get; set; } = new List<Signature>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public void AddCard(Card card)  => this.Cards.Add(card);
        public void AddFlat(Flat flat, Card card)
        {
            //Debitar o valor do plano no cartao
            var merchant = new Domain.Transactions.ValueObject.Merchant() { Name = flat.Name };
            card.CreateTransaction(merchant, new Monetary(flat.Value), flat.Description);

            //Desativo caso tenha alguma assinatura ativa
            DisableActiveSigniture();

            //Adiciona uma nova assinatura
            this.Signatures.Add(new Signature()
            {
                Active = true,
                Flat = flat,
                DtActivation = DateTime.Now,
            });

        }
        protected void DisableActiveSigniture()
        {
            //Caso tenha alguma assintura ativa, destiva a assinatura
            if (this.Signatures.Count > 0 && this.Signatures.Any(x => x.Active))
                this.Signatures.FirstOrDefault(x => x.Active).Active = false;
        }

        protected String CryptoPasswrod(string senhaAberta)
        {
            SHA256 criptoProvider = SHA256.Create();

            byte[] btexto = Encoding.UTF8.GetBytes(senhaAberta);

            var criptoResult = criptoProvider.ComputeHash(btexto);

            return Convert.ToHexString(criptoResult);
        }
    }
}
