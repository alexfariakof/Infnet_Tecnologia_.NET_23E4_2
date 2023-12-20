using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Notifications;
using Repository.Mapping.Notifications;
using __mock__;


namespace Repository.Mapping
{
    public class NotificationMapTest
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
                var configuration = new NotificationMap();

                configuration.Configure(builder.Entity<Notification>());

                var model = builder.Model;
                var entityType = model.FindEntityType(typeof(Notification));

                // Act
                var idProperty = entityType.FindProperty("Id");
                var titleroperty = entityType.FindProperty("Title");
                var messageProperty = entityType.FindProperty("Message");
                var dtNotificationProperty = entityType.FindProperty("DtNotification");
                var notificationTypeProperty = entityType.FindProperty("NotificationType");

                // Assert
                Assert.NotNull(idProperty);
                Assert.NotNull(titleroperty);
                Assert.NotNull(messageProperty);
                Assert.NotNull(dtNotificationProperty);
                Assert.NotNull(notificationTypeProperty);

                Assert.True(idProperty.IsPrimaryKey());
                Assert.False(titleroperty.IsNullable);
                Assert.Equal(150, titleroperty.GetMaxLength());
                Assert.False(messageProperty.IsNullable);
                Assert.Equal(250, messageProperty.GetMaxLength());
                Assert.False(dtNotificationProperty.IsNullable);
                Assert.False(notificationTypeProperty.IsNullable);
            }
        }
    }
}
