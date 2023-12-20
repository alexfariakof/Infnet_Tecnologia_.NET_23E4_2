using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Repository.Mapping.Streaming;
using __mock__;

namespace Repository.Tests.Mapping.Streaming
{
    public class MusicMapTest
    {
        [Fact]
        public void EntityConfiguration_IsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MockRegisterContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase_MusicMap")
                .Options;

            using (var context = new MockRegisterContext(options))
            {
                var builder = new ModelBuilder(new ConventionSet());
                var configuration = new MusicMap();

                configuration.Configure(builder.Entity<Music<Playlist>>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Music<Playlist>));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var nameProperty = entityType.FindProperty("Name");
                var durationProperty = entityType.FindNavigation("Duration").ForeignKey.Properties.First();

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(nameProperty);
                Assert.NotNull(durationProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(nameProperty.IsNullable);
                Assert.Equal(50, nameProperty.GetMaxLength());
                Assert.False(durationProperty.IsNullable);
            }
        }
    }
}
