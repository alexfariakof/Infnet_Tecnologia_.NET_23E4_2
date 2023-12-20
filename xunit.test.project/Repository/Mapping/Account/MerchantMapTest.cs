using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using __mock__;

namespace Repository.Mapping
{
    public class MerchantMapTest
    {
        [Fact]
        public void EntityConfiguration_IsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MockRegisterContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var context = new MockRegisterContext(options))
            {
                var builder = new ModelBuilder(new ConventionSet());
                var configuration = new MerchantMap();

                configuration.Configure(builder.Entity<Merchant>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Merchant));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var nameProperty = entityType.FindProperty("Name");
                var emailProperty = entityType.FindProperty("Email");
                var passwordProperty = entityType.FindProperty("Password");
                var cnpjProperty = entityType.FindProperty("CNPJ");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(nameProperty);
                Assert.NotNull(emailProperty);
                Assert.NotNull(passwordProperty);
                Assert.NotNull(cnpjProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(nameProperty.IsNullable);
                Assert.Equal(100, nameProperty.GetMaxLength());
                Assert.False(emailProperty.IsNullable);
                Assert.Equal(150, emailProperty.GetMaxLength());
                Assert.False(passwordProperty.IsNullable);
                Assert.Equal(100, passwordProperty.GetMaxLength());
                Assert.False(cnpjProperty.IsNullable);
                Assert.Equal(18, cnpjProperty.GetMaxLength());
            }
        }
    }
}
