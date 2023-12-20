using __mock__;
using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Moq;

namespace Domain.Account
{
    public class CustomerTest
    {
        [Fact]
        public void Should_Create_Account_With_Flat_Card_And_Playlist()
        {
            // Arrange
            var customerMock = new Mock<Customer>();
            customerMock.VerifyAll();
            var customer = customerMock.Object;
            var flat = new Flat
            {
                Id = Guid.NewGuid(),
                Name = "Test Flat",
                Value = 50.0m,
                Description = "Test Description"
            };

            var card = MockCard.GetFaker();
            card.Active = true;

            var openPassword = "12345!";

            // Act
            customer.CreateAccount("John Doe", "john@example.com", openPassword, "123456789", DateTime.Now, flat, card);

            // Assert
            Mock.Verify(customerMock);
            Assert.Equal("John Doe", customer.Name);
            Assert.Equal("john@example.com", customer.Email);
            Assert.Equal(customer.CryptoPasswrod(openPassword), customer.Password) ;
            Assert.Equal("123456789", customer.CPF);
            Assert.Equal(DateTime.Now.Date, customer.Birth.Date);
            Assert.Single(customer.Cards, card);
            Assert.Single(customer.Playlists);
        }
    }
}