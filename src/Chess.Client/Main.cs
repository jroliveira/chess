namespace Chess.Client
{
    using System.Threading.Tasks;
    using static Chess.Client.Infra.GameServerFactory;
    using static System.Console;
    using static System.Text.Encoding;

    internal class Main
    {
        public async Task Start()
        {
            Title = "Chess";
            OutputEncoding = GetEncoding(65001);

            var gameServer = await ConnectAndGetGameServer().ConfigureAwait(false);
            await gameServer.Start().ConfigureAwait(false);

            while (true)
            {
            }
        }
    }
}
