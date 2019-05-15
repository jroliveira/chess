namespace Chess.SiloHost
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Orleans.Configuration;
    using Orleans.Hosting;

    using static System.Console;
    using static System.Net.IPAddress;

    public class Program
    {
        public static int Main(string[] args) => RunMain()
            .GetAwaiter()
            .GetResult();

        private static async Task<int> RunMain()
        {
            Title = "Chess Silo Host";

            try
            {
                var silo = CreateSilo();
                await silo.StartAsync();

                WriteLine("Press Enter to terminate...");
                ReadLine();

                await silo.StopAsync();

                return 0;
            }
            catch (Exception exception)
            {
                WriteLine(exception);

                return 1;
            }
        }

        private static ISiloHost CreateSilo() => new SiloHostBuilder()
            .UseLocalhostClustering()
            .Configure<ClusterOptions>(options => options.ServiceId = "Chess")
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = Loopback)
            .ConfigureLogging(logging => logging.AddConsole())
            .Build();
    }
}
