using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Timers;

namespace caffetogo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            Timer clock = new Timer(86400000);
            clock.Start();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
