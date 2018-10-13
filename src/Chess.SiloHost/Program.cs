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
        public static int Main(string[] args) => RunMain().Result;

        private static async Task<int> RunMain()
        {
            try
            {
                var host = await StartSilo().ConfigureAwait(false);
                WriteLine("Press Enter to terminate...");
                ReadLine();

                await host.StopAsync().ConfigureAwait(false);

                return 0;
            }
            catch (Exception ex)
            {
                WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Chess";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = Loopback)
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync().ConfigureAwait(false);
            return host;
        }
    }
}
