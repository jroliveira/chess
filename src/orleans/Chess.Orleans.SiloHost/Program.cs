namespace Chess.Orleans.SiloHost
{
    using System;
    using System.Threading.Tasks;

    using global::Orleans.Configuration;
    using global::Orleans.Hosting;

    using Microsoft.Extensions.Logging;

    using static System.Console;
    using static System.Net.IPAddress;

    using static Microsoft.Extensions.Logging.LogLevel;

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

                WriteLine("Press Enter to terminate");
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
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "chess-cluster";
                options.ServiceId = "chess-service";
            })
            .ConfigureLogging(logging => logging.AddConsole().SetMinimumLevel(Warning))
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = Loopback)
            .Build();
    }
}
