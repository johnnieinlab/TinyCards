using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TinyCards.Core.Data;
using TinyCards.Core.Config;
using TinyCards.Core.Config.Extensions;
using TinyCards.Core.Interface;
using TinyCards.Core.Service;

namespace TinyCards.Core.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(
            this IServiceCollection @this, IConfiguration configuration)
        {
            @this.AddSingleton<AppConfig>(
                configuration.ReadAppConfiguration());

            // AddScoped
            @this.AddDbContext<CardDbContext>(
                (serviceProvider, optionsBuilder) => {
                    var appConfig = serviceProvider.GetRequiredService<AppConfig>();

                    optionsBuilder.UseSqlServer(appConfig.connectionstring);
                });

            @this.AddScoped<ICardService, CardService>();
        }
    }
}
