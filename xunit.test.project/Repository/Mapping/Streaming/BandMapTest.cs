﻿using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Repository.Mapping.Streaming;
using __mock__;

namespace Repository.Mapping;
public class BandMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        const int PROPERTY_COUNT = 5;

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
            var propsCount = entityType.GetNavigations().Count() + entityType.GetProperties().Count();

            // Act
            var idProperty = entityType.FindProperty("Id");
            var nameProperty = entityType.FindProperty("Name");
            var descriptionProperty = entityType.FindProperty("Description");
            var backdropProperty = entityType.FindProperty("Backdrop");
            var albumNavigation = entityType.FindNavigation("Albums");

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
            Assert.NotNull(albumNavigation);
            Assert.True(albumNavigation.IsCollection);
            Assert.NotNull(albumNavigation.ForeignKey.DeleteBehavior);
            var foreignKey = albumNavigation.ForeignKey;
            Assert.NotNull(foreignKey);
            Assert.Equal(DeleteBehavior.Cascade, foreignKey.DeleteBehavior);
            Assert.Equal(PROPERTY_COUNT, propsCount);
        }
    }
}