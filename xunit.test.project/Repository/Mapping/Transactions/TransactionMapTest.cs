using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Transactions.Agreggates;
using __mock__;

namespace Repository.Mapping.Transactions
{
    public class TransactionMapTest
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
                var configuration = new TransactionMap();

                configuration.Configure(builder.Entity<Transaction>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Transaction));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var dtTransactionProperty = entityType.FindProperty("DtTransaction");
                var descriptionProperty = entityType.FindProperty("Description");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(dtTransactionProperty);
                Assert.NotNull(descriptionProperty);

                Assert.True(idProperty.IsPrimaryKey());                
                Assert.False(dtTransactionProperty.IsNullable);                
                Assert.False(descriptionProperty.IsNullable);
                Assert.Equal(50, descriptionProperty.GetMaxLength());
            }
        }
    }
}