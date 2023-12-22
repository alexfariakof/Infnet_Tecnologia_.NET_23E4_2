using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Moq;
using __mock__;
using Bogus.DataSets;
using Domain.Account.Agreggates.Strategy;
using Domain.Account.Agreggates.Interfaces;

namespace Domain.Account
{
    public class CustomerTest
    {
        [Fact]
        public void Should_Create_Account_With_Flat_Card_And_Playlist()
        {
            // Arrange
            var  customerMock = new Mock<ICustomer>();
            var customer = MockCustomer.GetFaker();
            customerMock.Setup(c => c.Id).Returns(customer.Id);
            customerMock.Setup(c => c.Name).Returns(customer.Name);
            customerMock.Setup(c => c.Login).Returns(customer.Login);
            customerMock.Setup(c => c.CPF).Returns(customer.CPF);
            customerMock.Setup(c => c.Birth).Returns(customer.Birth);
            
            var flat = new Flat
            {
                Id = Guid.NewGuid(),
                Name = "Test Flat",
                Value = 50.0m,
                Description = "Test Description"
            };

            var card = MockCard.GetFaker();
            card.Active = true;

            // Act
            customer.CreateAccount(customer.Name, customer.Login, customer.Birth, customer.CPF, flat , card);
            // Assert
            Mock.Verify(customerMock);
            Assert.Equal(customerMock.Object.Name, customer.Name);
            Assert.Equal(customerMock.Object.Login, customer.Login) ;
            Assert.Equal(customerMock.Object.CPF, customer.CPF);
            Assert.Equal(customerMock.Object.Birth.Date, customer.Birth.Date);
            Assert.Single(customer.Cards, card);
            Assert.Single(customer.Playlists);
        }
    }
}