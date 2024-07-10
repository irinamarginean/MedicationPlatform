using DataAccessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace MedicationPlatform.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                 .Build()
                 .MigrateDatabase<DataContext>()
                 .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    int port = int.Parse(Environment.GetEnvironmentVariable("PORT"));
                    // int port = 5001;
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(port,
                            listenOptions =>
                            {
                                listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
                            });
                    });
                    webBuilder.UseStartup<Startup>().UseKestrel(options => options.AllowSynchronousIO = true).UseUrls($"http://0.0.0.0:{port}");
                });
    }
}
