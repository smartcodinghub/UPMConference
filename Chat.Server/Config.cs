using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Chat.Server
{
    public static class Config
    {
        /// <summary>
        /// Obtiene la configuración de hosting (Kestrel)
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetHostingConfiguration()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddJsonFile($"hosting.{env}.json", optional: true)
                .Build();
        }
    }
}
