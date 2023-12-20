using Domain.Core.Interfaces;

namespace Domain.Transactions.ValueObject
{
    public record Merchant : IMerchant
    {
        public string Name { get; set; }
        public string CNPJ { get ; set ; }
    }
}
