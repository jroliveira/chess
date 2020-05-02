namespace Chess.Orleans.ConsoleApp
{
    using System.Threading.Tasks;

    using Chess.Infra.Monad;

    using global::Orleans;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.GameData;
    using static Chess.Orleans.ConsoleApp.GameMenuItem;
    using static Chess.Orleans.ConsoleApp.Infra.Orleans.ClusterFactory;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    public class Program
    {
        public static async Task Main()
        {
            SetupScreen();

            var cluster = await CreateCluster((attempt, attemptsBeforeFailing) => ShowInfo($"Attempt {attempt} of {attemptsBeforeFailing} to start the game", clean: false));

            await cluster.Match(
                exception => Task(ShowError(exception)),
                ShowScenario);
        }

        private static async Task<Unit> ShowScenario(IClusterClient clusterClient)
        {
            var gameData = CreateGameData()
                .SetClusterClient(Some(clusterClient))
                .SetGameMenuItem(MainMenu);

            while (gameData.GameMenuItem != Default)
            {
                gameData = await gameData.ExecuteOption();
            }

            while (true)
            {
            }
        }
    }
}
