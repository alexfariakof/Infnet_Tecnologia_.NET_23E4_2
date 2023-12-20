namespace Domain.Transactions.ValueObject
{
    public record ExpiryDate
    {
        public static implicit operator (int Month, int Year)(ExpiryDate expiryDate) => (expiryDate.Month, expiryDate.Year);
        public static implicit operator ExpiryDate((int Month, int Year) values) => new ExpiryDate(values.Month, values.Year);
        public int Month { get; init; }
        public int Year { get; init; }
        public String Value { get { return this.Formatted_ptBr(); } }
        public ExpiryDate(int month, int year)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentException("O mês deve ser entre 1 e 12.", nameof(month));
            }

            Month = month;
            Year = year;
        }
        public string Formatted_ptBr()
        {
            return $"{Month:D2}/{Year % 100:D2}";
        }
    }
}