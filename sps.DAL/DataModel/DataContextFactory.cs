using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using sps.Domain.Model.Services;
using System;
using System.IO;

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

            // Create a simple encryption service for design-time use
            var encryptionService = new DesignTimeEncryptionService(configuration);

            // Create and return the SpsDbContext
            return new SpsDbContext(builder.Options, encryptionService);
        }
    }

    // Simple encryption service for design-time contexts
    public class DesignTimeEncryptionService : IEncryptionService
    {
        private readonly string _key;
        private readonly string _iv;

        public DesignTimeEncryptionService(IConfiguration configuration)
        {
            _key = configuration["Encryption:Key"] ?? throw new ArgumentNullException("Encryption:Key not configured");
            _iv = configuration["Encryption:IV"] ?? throw new ArgumentNullException("Encryption:IV not configured");
        }

        public string Encrypt(string plainText)
        {
            // Design-time operations don't need real encryption
            return plainText;
        }

        public string Decrypt(string cipherText)
        {
            // Design-time operations don't need real decryption
            return cipherText;
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