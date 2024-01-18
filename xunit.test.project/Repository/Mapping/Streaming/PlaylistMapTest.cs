﻿using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Repository.Mapping.Streaming;
using __mock__;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Repository.Mapping;
public class PlaylistMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        const int PROPERTY_COUNT = 2;
        // Arrange
        var options = new DbContextOptionsBuilder<MockRegisterContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase_PlaylistMapTest")
            .Options;

        using (var context = new MockRegisterContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new PlaylistMap();

            configuration.Configure(builder.Entity<Playlist>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(Playlist));
            var propsCount = entityType.GetNavigations().Count() + entityType.GetProperties().Count();

            // Act
            var idProperty = entityType.FindProperty("Id");
            var nameProperty = entityType.FindProperty("Name");

            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(nameProperty);

            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(nameProperty.IsNullable);
            Assert.Equal(50, nameProperty.GetMaxLength());
            Assert.Equal(PROPERTY_COUNT, propsCount);
        }
    }
}
