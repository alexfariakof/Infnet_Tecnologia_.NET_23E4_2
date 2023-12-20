using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Repository.Mapping.Streaming;
using __mock__;

namespace Repository.Mapping
{
    public class FlatMapTest
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
                var configuration = new FlatMap();

                configuration.Configure(builder.Entity<Flat>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Flat));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var nameProperty = entityType.FindProperty("Name");
                var descriptionProperty = entityType.FindProperty("Description");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(nameProperty);
                Assert.NotNull(descriptionProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(nameProperty.IsNullable);
                Assert.Equal(50, nameProperty.GetMaxLength());
                Assert.False(descriptionProperty.IsNullable);
                Assert.Equal(1024, descriptionProperty.GetMaxLength());
            }
        }
    }
}
