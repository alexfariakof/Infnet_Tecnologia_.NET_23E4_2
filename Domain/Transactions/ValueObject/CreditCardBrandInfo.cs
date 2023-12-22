using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Domain.Transactions.ValueObject
{
    public record CreditCardBrandInfo
    {
        [NotMapped]
        public CreditCardBrand Brand { get; }        
        public string Name { get; }
        public bool IsValid { get; }

        private CreditCardBrandInfo(string name)
        {
            Name = name;
        }

        private CreditCardBrandInfo(CreditCardBrand brand, string name, bool isValid)
        {
            Brand = brand;
            Name = name;
            IsValid = isValid;
        }

        public static CreditCardBrandInfo IdentifyCard(string creditCardNumber)
        {
            string cleanedNumber = RemoveNonNumericCharacters(creditCardNumber);
            bool isValid = IsCreditCardValid(cleanedNumber);

            if (Regex.IsMatch(cleanedNumber, @"^4[0-9]{12}(?:[0-9]{3})?$"))
            {
                return new CreditCardBrandInfo(CreditCardBrand.Visa, "Visa", isValid);
            }
            else if (Regex.IsMatch(cleanedNumber, @"^5[1-5][0-9]{14}$|^2(?:2(?:2[1-9]|[3-9][0-9])|[3-6][0-9][0-9]|7(?:[01][0-9]|20))[0-9]{12}$"))
            {
                return new CreditCardBrandInfo(CreditCardBrand.Mastercard, "Mastercard", isValid);
            }
            else if (Regex.IsMatch(cleanedNumber, @"^3[47][0-9]{13}$"))
            {
                return new CreditCardBrandInfo(CreditCardBrand.Amex, "Amex", isValid);
            }
            else if (Regex.IsMatch(cleanedNumber, @"^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$"))
            {
                return new CreditCardBrandInfo(CreditCardBrand.Discover, "Discover", isValid);
            }
            else if (Regex.IsMatch(cleanedNumber, @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$"))
            {
                return new CreditCardBrandInfo(CreditCardBrand.DinersClub, "Diners Club", isValid);
            }
            else if (Regex.IsMatch(cleanedNumber, @"^(?:2131|1800|35[0-9]{3})[0-9]{11}$"))
            {
                return new CreditCardBrandInfo(CreditCardBrand.JCB, "JCB", isValid);
            }
            else
            {
                return new CreditCardBrandInfo(CreditCardBrand.Invalid, "com bandeira desconhecida.", false);
            }
        }
        private static bool IsCreditCardValid(string numeroCartao)
        {
            int soma = 0;
            bool alternar = false;

            for (int i = numeroCartao.Length - 1; i >= 0; i--)
            {
                int digito = numeroCartao[i] - '0';

                if (alternar)
                {
                    digito *= 2;

                    if (digito > 9)
                    {
                        digito -= 9;
                    }
                }

                soma += digito;
                alternar = !alternar;
            }

            return soma % 10 == 0;
        }

        private static string RemoveNonNumericCharacters(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
        public enum CreditCardBrand
        {
            Visa,
            Mastercard,
            Amex,
            Discover,
            DinersClub,
            JCB,
            Invalid
        }
    }
}
