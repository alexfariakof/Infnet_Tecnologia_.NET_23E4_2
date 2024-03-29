﻿using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Domain.Account.Agreggates;
using __mock__;

namespace Repository.Mapping
{
    public class MusicPersonalMapTest
    {
        [Fact]
        public void EntityConfiguration_IsValid()
        {
            const int PROPERTY_COUNT = 3;
            // Arrange
            var options = new DbContextOptionsBuilder<MockRegisterContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase_MusicMap")
                .Options;

            using (var context = new MockRegisterContext(options))
            {
                var builder = new ModelBuilder(new ConventionSet());
                var configuration = new MusicPersonalMap();

                configuration.Configure(builder.Entity<Music<PlaylistPersonal>>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Music<PlaylistPersonal>));
                var propsCount = entityType.GetNavigations().Count() + entityType.GetProperties().Count();

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
                Assert.Equal(PROPERTY_COUNT, propsCount);
            }
        }
    }
}
