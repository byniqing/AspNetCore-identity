using IdentityDemo.Date;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IdentityDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .MigrationDbContext<ApplicationDbContext>((context, service) =>
                {
                    new ApplicationDbContextSeed().AsyncSpeed(context, service).Wait();
                })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
