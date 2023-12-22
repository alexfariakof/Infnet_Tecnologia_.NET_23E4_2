using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Repository.Mapping.Account;
using __mock__;

namespace Repository.Mapping
{
    public class CustomerMapTest
    {
        [Fact]
        public void EntityConfiguration_IsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MockRegisterContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase_CustomerMapTest")
                .Options;

            using (var context = new MockRegisterContext(options))
            {
                var builder = new ModelBuilder(new ConventionSet());
                var configuration = new CustomerMap();

                configuration.Configure(builder.Entity<Customer>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Customer));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var nameProperty = entityType.FindProperty("Name");
                var loginNavigation = entityType.FindNavigation(nameof(Customer.Login));
                var emailProperty = loginNavigation.TargetEntityType.FindProperty(nameof(Login.Email));
                var passwordProperty = loginNavigation.TargetEntityType.FindProperty(nameof(Login.Password));
                var birthProperty = entityType.FindProperty("Birth");
                var cpfProperty = entityType.FindProperty("CPF");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(nameProperty);
                Assert.NotNull(loginNavigation);
                Assert.NotNull(emailProperty);
                Assert.NotNull(passwordProperty);
                Assert.NotNull(birthProperty);
                Assert.NotNull(cpfProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(nameProperty.IsNullable);
                Assert.Equal(100, nameProperty.GetMaxLength());
                Assert.False(emailProperty.IsNullable);
                Assert.Equal(150, emailProperty.GetMaxLength());
                Assert.False(passwordProperty.IsNullable);
                Assert.Equal(255, passwordProperty.GetMaxLength());
                Assert.False(birthProperty.IsNullable);
                Assert.False(cpfProperty.IsNullable);
                Assert.Equal(14, cpfProperty.GetMaxLength());
            }
        }
    }
}