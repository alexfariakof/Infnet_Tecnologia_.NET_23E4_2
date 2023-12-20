using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using __mock__;

namespace Repository.Mapping.Streaming
{
    public class AlbumMapTest
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
                var configuration = new AlbumMap();

                configuration.Configure(builder.Entity<Album>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Album));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var nameProperty = entityType.FindProperty("Name");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(nameProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(nameProperty.IsNullable);
                Assert.Equal(50, nameProperty.GetMaxLength());
            }
        }
    }
}
