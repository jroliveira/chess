namespace Chess.SiloHost
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Orleans.Configuration;
    using Orleans.Hosting;

    public class Program
    {
        public static int Main(string[] args) => RunMain().Result;

        private static async Task<int> RunMain()
        {
            try
            {
                var host = await StartSilo().ConfigureAwait(false);
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                await host.StopAsync().ConfigureAwait(false);

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback);

            var host = builder.Build();
            await host.StartAsync().ConfigureAwait(false);
            return host;
        }
    }
}
