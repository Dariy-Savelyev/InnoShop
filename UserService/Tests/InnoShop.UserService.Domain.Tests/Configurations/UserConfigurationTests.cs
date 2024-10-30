using InnoShop.UserService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserService.Domain.Tests.Configurations
{
    public class UserConfigurationTests
    {
        private readonly ApplicationContext _context;

        public UserConfigurationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            _context = new ApplicationContext(options);
        }

        [Fact]
        public void Configure_ShouldSetPrimaryKey()
        {
            // Arrange & Act
            var entityType = _context.Model.FindEntityType(typeof(User));

            // Assert
            var primaryKey = entityType!.FindPrimaryKey();
            Assert.Single(primaryKey!.Properties);
            Assert.Equal("Id", primaryKey.Properties[0].Name);
        }

        [Theory]
        [InlineData("Id", 36)]
        [InlineData("UserName", 255)]
        [InlineData("Email", 255)]
        [InlineData("Role", 255)]
        [InlineData("PasswordHash", 255)]
        [InlineData("EmailConfirmationToken", 36)]
        [InlineData("PasswordResetCodeToken", 255)]
        public void Configure_ShouldSetMaxLength(string propertyName, int expectedLength)
        {
            // Arrange & Act
            var entityType = _context.Model.FindEntityType(typeof(User));
            var property = entityType!.FindProperty(propertyName);

            // Assert
            Assert.NotNull(property);
            Assert.Equal(expectedLength, property.GetMaxLength());
        }

        [Fact]
        public void Configure_ShouldSetUniqueIndexForUserName()
        {
            // Arrange & Act
            var entityType = _context.Model.FindEntityType(typeof(User));
            var index = entityType!.FindIndex(entityType.FindProperty("UserName")!);

            // Assert
            Assert.NotNull(index);
            Assert.True(index.IsUnique);
        }

        [Fact]
        public void Configure_ShouldSetUniqueIndexForEmail()
        {
            // Arrange & Act
            var entityType = _context.Model.FindEntityType(typeof(User));
            var index = entityType!.FindIndex(entityType.FindProperty("Email")!);

            // Assert
            Assert.NotNull(index);
            Assert.True(index.IsUnique);
        }

        [Fact]
        public void Configure_ShouldSetIsEmailConfirmedProperty()
        {
            // Arrange & Act
            var entityType = _context.Model.FindEntityType(typeof(User));
            var property = entityType!.FindProperty("IsEmailConfirmed");

            // Assert
            Assert.NotNull(property);
            Assert.Equal(typeof(bool), property.ClrType);
        }

        [Fact]
        public void Configure_ShouldNotAllowNullForRequiredProperties()
        {
            // Arrange & Act
            var entityType = _context.Model.FindEntityType(typeof(User));

            // Assert
            var requiredProperties = new[] { "Id", "UserName", "Email", "Role", "PasswordHash" };
            foreach (var propertyName in requiredProperties)
            {
                var property = entityType!.FindProperty(propertyName);
                Assert.False(property!.IsNullable);
            }
        }
    }
}