namespace Chess.Client
{
    using System.Threading.Tasks;

    using static System.Console;
    using static System.Text.Encoding;
    using static System.Threading.Tasks.Task;

    using static Chess.Client.Infra.Orleans.ClusterFactory;
    using static Chess.Client.Infra.Orleans.PlayerFactory;
    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Client.MenuOption;

    internal class Main
    {
        public async Task Start()
        {
            Title = "Chess";
            OutputEncoding = GetEncoding(65001);

            var cluster = await CreateCluster();

            await cluster.Match(
                exception =>
                {
                    WriteError(exception.Message);

                    return CompletedTask;
                },
                async grainFactory =>
                {
                    var player = await CreatePlayerWith(grainFactory);

                    var menu = new Menu(player);
                    await menu.Show(
                        GetMenuOption(grainFactory),
                        match => match.Start());

                    while (true)
                    {
                    }
                });
        }
    }
}
