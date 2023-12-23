﻿using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Repository.Mapping.Streaming;
using __mock__;

namespace Repository.Mapping
{
    public class AlbumMapTest
    {
        [Fact]
        public void EntityConfiguration_IsValid()
        {
            const int PROPERTY_COUNT = 3;

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
                var propsCount = entityType.GetNavigations().Count() + entityType.GetProperties().Count();

                // Act
                var idProperty = entityType.FindProperty("Id");
                var nameProperty = entityType.FindProperty("Name");
                var musicNavigation = entityType.FindNavigation("Music");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(nameProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(nameProperty.IsNullable);
                Assert.Equal(50, nameProperty.GetMaxLength());
                Assert.NotNull(musicNavigation);
                Assert.True(musicNavigation.IsCollection);
                Assert.NotNull(musicNavigation.ForeignKey.DeleteBehavior);
                var foreignKey = musicNavigation.ForeignKey;
                Assert.NotNull(foreignKey);
                Assert.Equal(DeleteBehavior.Cascade, foreignKey.DeleteBehavior);
                Assert.Equal(PROPERTY_COUNT, propsCount);
            }
        }
    }
}