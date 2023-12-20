using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using __mock__;

namespace Repository.Mapping
{
    public class SignitureMapTest
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
                var configuration = new SignitureMap();

                configuration.Configure(builder.Entity<Signature>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Signature));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var activeProperty = entityType.FindProperty("Active");
                var dtActivationProperty = entityType.FindProperty("DtActivation");


                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(activeProperty);
                Assert.NotNull(dtActivationProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(activeProperty.IsNullable);
                Assert.False(dtActivationProperty.IsNullable);
            }
        }
    }
}
