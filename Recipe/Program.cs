using Business.Implementations;
using Business.Interfaces;
using Persistence.Implementations;
using Persistence.Interfaces;

namespace Recipe;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args)
                     .Build();

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });

}