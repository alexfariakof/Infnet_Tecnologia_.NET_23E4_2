using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Transactions.ValueObject
{
    public record ExpiryDate
    {
        public static implicit operator (int Month, int Year)(ExpiryDate expiryDate) => (expiryDate.Month, expiryDate.Year);
        public static implicit operator ExpiryDate((int Month, int Year) values) => new ExpiryDate(values);
        public DateTime Value { get; set; }
        
        [NotMapped]
        public int Month { get; init; }
        
        [NotMapped]
        public int Year { get; init; }
       
        public ExpiryDate(DateTime value)
        {
            if (value.Month < 1 || value.Month > 12)
            {
                throw new ArgumentException("O mês deve ser entre 1 e 12.", nameof(value.Month));
            }

            Value = value;
            Month = value.Month;
            Year = value.Year;

        }
        public string Formatted_ptBr()
        {
            return $"{Value.Month:D2}/{Value.Year % 100:D2}";
        }
    }
}