using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Moq;

namespace Domain.Account
{
    public class MerchantTest
    {
        [Fact]
        public void Should_Create_Account_With_Flat_Card_And_Playlist()
        {
            // Arrange
            var merchantMock = new Mock<Merchant>();
            merchantMock.VerifyAll();
            var customer = merchantMock.Object;
            var flat = new Flat
            {
                Id = Guid.NewGuid(),
                Name = "Test Flat",
                Value = 50.0m,
                Description = "Test Description"
                
            };

            var card = new Card
            {
                Id = Guid.NewGuid(),
                Number = "999-999-999",
                Active = true,
                Limit = 1000m
            };
            var openPassword = "12345!";

            // Act
            customer.CreateAccount("John Doe", "john@example.com", openPassword, "123456789", flat, card);

            // Assert
            Mock.Verify(merchantMock);
            Assert.Equal("John Doe", customer.Name);
            Assert.Equal("john@example.com", customer.Email);
            Assert.Equal(customer.CryptoPasswrod(openPassword), customer.Password) ;
            Assert.Equal("123456789", customer.CNPJ);
            Assert.Single(customer.Cards, card);
        }
    }
}