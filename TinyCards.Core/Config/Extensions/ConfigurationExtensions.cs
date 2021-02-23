using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TinyCards.Core.Config.Extensions
{
    public static class ConfigurationExtensions
    {
        public static AppConfig ReadAppConfiguration(
    this IConfiguration @this)
        {
            var connectionString = @this.GetConnectionString("CardDatabase");

            return new AppConfig()
            {
                connectionstring = connectionString
            };
        }
    }
}       
