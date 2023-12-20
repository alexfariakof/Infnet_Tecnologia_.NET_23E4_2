using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using __mock__;

namespace Repository.Mapping.Streaming
{
    public class BandMapTest
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
                var configuration = new BandMap();

                configuration.Configure(builder.Entity<Band>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Band));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var nameProperty = entityType.FindProperty("Name");
                var descriptionProperty = entityType.FindProperty("Description");
                var backdropProperty = entityType.FindProperty("Backdrop");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(nameProperty);
                Assert.NotNull(descriptionProperty);
                Assert.NotNull(backdropProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(nameProperty.IsNullable);
                Assert.Equal(50, nameProperty.GetMaxLength());
                Assert.False(descriptionProperty.IsNullable);
                Assert.Equal(50, descriptionProperty.GetMaxLength());
                Assert.False(backdropProperty.IsNullable);
                Assert.Equal(50, backdropProperty.GetMaxLength());
            }
        }
    }
}
