using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Chat.Server
{
    public class Program
    {
        public static void Main()
        {
            BuildWebHost().Run();
        }

        /// <summary>
        /// Construye el servidor
        /// </summary>
        /// <returns></returns>
        public static IWebHost BuildWebHost()
        {
            IWebHostBuilder builder = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(Config.GetHostingConfiguration())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseDefaultServiceProvider((context, options) => options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                .UseStartup<Startup>();

            return builder.Build();
        }
    }
}
