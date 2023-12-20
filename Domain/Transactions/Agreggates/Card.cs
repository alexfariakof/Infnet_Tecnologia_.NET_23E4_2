using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Transactions.ValueObject;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Transactions.Agreggates
{
    public class Card : BaseModel
    {
        private const int INTERVAL_TRANSACTON = -2;
        private const int REPEAT_TRANSACTON_MERCHANT = 1;
        public Boolean Active { get; set; }
        public Monetary Limit { get; set; }
        public String Number { get; set; }
        public ExpiryDate Validate { get; set; }
        
        private string _cvv;
        public string CVV
        {
            get { return _cvv; }
            set { _cvv = CryptoCVV(value); }
        }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void CreateTransaction(Merchant merchant, Monetary value, string description = "")
        {
            // Verifica se o cartão está ativo
            this.IsCardActive();

            Transaction transaction = new Transaction
            {
                Merchant = merchant,
                Value = value,
                Description = description,
                DtTransaction = DateTime.Now,
            };

            // Verifica Limite Disponivel 
            this.VerifyLimit(transaction);

            // Verificãção de Regras Antifraude
            this.ValidateTransaction(transaction);

            // Cria um numero de autorização 
            transaction.Id = Guid.NewGuid();

            // Diminui Limite com o valor da tranzação 
            this.Limit -= transaction.Value;

            this.Transactions.Add(transaction);
        }

        private void ValidateTransaction(Transaction transaction)
        {
            var lastTransactions = this.Transactions.Where(t => t.DtTransaction > DateTime.Now.AddMinutes(INTERVAL_TRANSACTON));

            if (lastTransactions?.Count() >= 3)
                throw new Exception("Cartão utilizado muitas vezes em um período curto");

            var transactionRepetedByMerchant = lastTransactions?
                                                .Where(x => x.Merchant.Name.ToUpper().Equals(transaction.Merchant.Name.ToUpper())
                                                && x.Value == transaction.Value)
                                                .Count() >= REPEAT_TRANSACTON_MERCHANT;

            if (transactionRepetedByMerchant)
                throw new Exception("Transacao Duplicada para o mesmo cartão e o mesmo Comerciante");
        }

        private void VerifyLimit(Transaction transaction)
        {
            if (this.Limit < transaction.Value) 
            {
                throw new Exception("Cartão não possui limite para esta transação.");
            }

        }
        public String CryptoCVV(string openCVV)
        {
            SHA256 criptoProvider = SHA256.Create();

            byte[] btexto = Encoding.UTF8.GetBytes(openCVV);

            var criptoResult = criptoProvider.ComputeHash(btexto);

            return Convert.ToHexString(criptoResult);
        }
        private void IsCardActive()
        {
            if (!this.Active)
            {
                throw new Exception("Cartão não está ativo.");
            }
        }
    }
}
