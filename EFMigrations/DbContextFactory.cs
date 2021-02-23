using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TinyCards.Core.Config.Extensions;
using TinyCards.Core.Data;

namespace EFMigrations
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CardDbContext>
    {
        public CardDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var config = configuration.ReadAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<CardDbContext>();

            optionsBuilder.UseSqlServer(
                config.connectionstring,
                options => {
                    options.MigrationsAssembly("EFMigrations");
                });

            return new CardDbContext(optionsBuilder.Options);
        }
    }
}
