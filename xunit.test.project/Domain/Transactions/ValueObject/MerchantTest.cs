using Domain.Transactions.ValueObject;

namespace Domain.Transactions
{
    public class MerchantRecordTest
    {
        [Fact]
        public void Should_Set_Properties_Correctly_Merchant()
        {
            // Arrange
            var name = "Test Merchant";
            var cnpj = "12345678901234";

            // Act
            var merchant = new Merchant(){ Name = name, CNPJ = cnpj };

            // Assert
            Assert.Equal(name, merchant.Name);
            Assert.Equal(cnpj, merchant.CNPJ);
        }
    }
}
