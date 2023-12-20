using Domain.Core.ValueObject;
using Domain.Transactions.Agreggates;
using Domain.Transactions.ValueObject;
using static Domain.Account.ValueObject.CreditCardBrandInfo;
using Bogus;

namespace __mock__
{
    public class MockCard
    {
        public static Card GetFaker()
        {
            var fakeCard = new Faker<Card>()
                .RuleFor(a => a.Id, f => f.Random.Guid())
                .RuleFor(a => a.Active, f => f.Random.Bool())
                .RuleFor(a => a.Limit, f => new Monetary(f.Random.Decimal(1000, 10000)))
                .RuleFor(a => a.Number, f => GenerateValidCreditCardNumber())
                .RuleFor(a => a.Validate, f => new ExpiryDate(f.Random.Int(1, 12), f.Random.Int(DateTime.Now.Year, DateTime.Now.Year + 5)))
                .RuleFor(a => a.CVV, f => f.Random.Int(100, 999).ToString())
                .Generate();

            return fakeCard;
        }

        public static List<Card> GetListFaker(int count)
        {
            var cardList = new List<Card>();
            for (var i = 0; i < count; i++)
            {
                cardList.Add(GetFaker());
            }
            return cardList;
        }

        private static string GenerateValidCreditCardNumber()
        {
            var cardNumber = GenerateRandomCreditCardNumber();
            var brandInfo = IdentifyCard(cardNumber);


            while (true)
            {
                cardNumber = GenerateRandomCreditCardNumber();
                brandInfo = IdentifyCard(cardNumber);
                if (brandInfo.Brand != CreditCardBrand.Invalid && brandInfo.IsValid != false)
                    break;
            }

            return cardNumber;
        }

        private static string GenerateRandomCreditCardNumber()
        {
            var random = new Random();
            var number = string.Empty;

            for (int i = 0; i < 16; i++)
            {
                number += random.Next(0, 14).ToString();
            }

            return number;
        }

        private static string GenerateRandomCreditCardNumber(CreditCardBrand brand)
        {
            var random = new Random();
            var number = string.Empty;

            switch (brand)
            {
                case CreditCardBrand.Visa:
                    number += "4";
                    break;
                case CreditCardBrand.Mastercard:
                    number += "5";
                    break;
                case CreditCardBrand.Amex:
                    number += "3";
                    break;
                case CreditCardBrand.Discover:
                    number += "6";
                    break;
                case CreditCardBrand.DinersClub:
                    number += "3";
                    break;
                case CreditCardBrand.JCB:
                    number += "35";
                    break;
                default:
                    break;
            }

            for (int i = number.Length; i < 16; i++)
            {
                number += random.Next(0, 10).ToString();
            }

            return number;
        }
    }
}