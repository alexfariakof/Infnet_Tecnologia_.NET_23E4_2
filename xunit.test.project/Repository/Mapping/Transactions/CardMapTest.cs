using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Transactions.Agreggates;
using Repository.Mapping.Transactions;
using __mock__;

namespace Repository.Mapping
{
    public class CardMapTest
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
                var configuration = new CardMap();

                configuration.Configure(builder.Entity<Card>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Card));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var activeProperty = entityType.FindProperty("Active");
                var numberProperty = entityType.FindProperty("Number");
                var limitProperty = entityType.FindProperty("Limit");
                
                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(activeProperty);
                Assert.NotNull(numberProperty);
                //Assert.NotNull(limitProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(activeProperty.IsNullable);
                Assert.False(numberProperty.IsNullable);
                Assert.Equal(100, numberProperty.GetMaxLength());
                //Assert.False(limitProperty.IsNullable);
            }
        }
    }
}
