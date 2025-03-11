using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace sps.DAL.DataModel
{
    public class SpsIdentityDbContextFactory : IDesignTimeDbContextFactory<SpsIdentityDbContext>
    {
        public SpsIdentityDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(Path.Combine(basePath, "..", "sps.API", "appsettings.json"))
                .Build();

            var builder = new DbContextOptionsBuilder<SpsIdentityDbContext>();
            var connectionString = configuration.GetConnectionString("IdentityConnection");
            builder.UseSqlServer(connectionString);

            return new SpsIdentityDbContext(builder.Options);
        }
    }
}