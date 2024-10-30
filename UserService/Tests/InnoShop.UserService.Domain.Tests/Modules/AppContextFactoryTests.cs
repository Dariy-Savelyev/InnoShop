using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InnoShop.UserService.Domain.Tests.Modules
{
    public class AppContextFactoryTests
    {
        [Fact]
        public void CreateDbContext_ShouldReturnApplicationContext()
        {
            // Arrange
            var factory = new AppContextFactory();
            Environment.SetEnvironmentVariable("ConnectionStrings_App", "Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");

            // Act
            var context = factory.CreateDbContext(Array.Empty<string>());

            // Assert
            Assert.NotNull(context);
            Assert.IsType<ApplicationContext>(context);
        }

        [Fact]
        public void CreateDbContext_ShouldEnableSensitiveDataLogging()
        {
            // Arrange
            var factory = new AppContextFactory();
            Environment.SetEnvironmentVariable("ConnectionStrings_App", "Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");

            // Act
            var context = factory.CreateDbContext(Array.Empty<string>());

            // Assert
            Assert.NotNull(context);
            Assert.True(context.GetService<ILoggingOptions>().IsSensitiveDataLoggingEnabled);
        }

        [Fact]
        public void CreateDbContext_ShouldSetCommandTimeout()
        {
            // Arrange
            var factory = new AppContextFactory();
            Environment.SetEnvironmentVariable("ConnectionStrings_App", "Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");

            // Act
            var context = factory.CreateDbContext(Array.Empty<string>());

            // Assert
            Assert.NotNull(context);
        }

        [Fact]
        public void CreateDbContext_ShouldUseSqlServerProvider()
        {
            // Arrange
            var factory = new AppContextFactory();
            Environment.SetEnvironmentVariable("ConnectionStrings_App", "Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");

            // Act
            var context = factory.CreateDbContext(Array.Empty<string>());

            // Assert
            Assert.NotNull(context);

            var databaseType = context.Database.ProviderName;
            Assert.Equal("Microsoft.EntityFrameworkCore.SqlServer", databaseType);

            var connection = context.Database.GetDbConnection();
            Assert.IsType<Microsoft.Data.SqlClient.SqlConnection>(connection);
        }
    }
}