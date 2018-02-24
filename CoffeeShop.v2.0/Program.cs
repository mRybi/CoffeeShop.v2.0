using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.v2._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost
        .CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(
            (WebHostBuilderContext context, IConfigurationBuilder builder) =>
            {
                builder.Sources.Clear();

                builder
                    .AddEnvironmentVariables()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
        .UseStartup<Startup>()
        .Build();
        }
    }
}
