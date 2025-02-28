using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace sps.DAL.DataModel
{
    public class SpsDbContextFactory : IDesignTimeDbContextFactory<SpsDbContext>
    {
        public SpsDbContext CreateDbContext(string[] args)
        {
            // Build the configuration
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(Path.Combine(basePath, "..", "sps.API", "appsettings.json"))
                .Build();

            // Create the DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<SpsDbContext>();
            var connectionString = configuration.GetConnectionString("SpsDbConnection");
            builder.UseSqlServer(connectionString);

            // Create and return the SpsDbContext
            return new SpsDbContext(builder.Options);
        }
    }

    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            // Build the configuration
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(Path.Combine(basePath, "..", "sps.API", "appsettings.json"))
                .Build();

            // Create the DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<IdentityDbContext>();
            var connectionString = configuration.GetConnectionString("IdentityConnection");
            builder.UseSqlServer(connectionString);

            // Create and return the IdentityDbContext
            return new IdentityDbContext(builder.Options);
        }
    }
}