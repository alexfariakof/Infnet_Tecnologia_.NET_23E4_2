using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Moq;
using __mock__;

namespace Domain.Account
{
    public class CustomerTest
    {
        [Fact]
        public void Should_Create_Account_With_Flat_Card_And_Playlist()
        {
            // Arrange
            var customerMock = MockCustomer.GetFaker();
            var customer = customerMock;
            var flat = new Flat
            {
                Id = Guid.NewGuid(),
                Name = "Test Flat",
                Value = 50.0m,
                Description = "Test Description"
            };

            var card = MockCard.GetFaker();
            card.Active = true;

            var login = MockLogin.GetFaker();

            // Act
            customer.CreateAccount("John Doe", login, DateTime.Now, "123456789", flat, card);

            // Assert
            Assert.Equal("John Doe", customer.Name);
            Assert.Equal(login, customer.Login) ;
            Assert.Equal("123456789", customer.CPF);
            Assert.Equal(DateTime.Now.Date, customer.Birth.Date);
            Assert.Single(customer.Cards, card);
            Assert.Single(customer.Playlists);
        }
    }
}