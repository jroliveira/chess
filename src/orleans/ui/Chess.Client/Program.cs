namespace Chess.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Client.Scenarios;

    using Orleans;

    using static System.Environment;
    using static System.Threading.Tasks.Task;

    using static Chess.Client.Infra.Orleans.ClusterFactory;
    using static Chess.Client.Infra.UI.Terminal;
    using static Chess.Client.Scenarios.CreateMatch.CreateMatchScenario;
    using static Chess.Client.Scenarios.CreatePlayer.CreatePlayerScenario;
    using static Chess.Client.Scenarios.MainMenu.MainMenuScenario;
    using static Chess.Client.Scenarios.SelectMatch.SelectMatchScenario;
    using static Chess.Lib.Monad.Utils.Util;

    public class Program
    {
        private static readonly IReadOnlyDictionary<int, Func<ScenarioData, Task<(int MenuOption, ScenarioData Data)>>> MenuOptions = new Dictionary<int, Func<ScenarioData, Task<(int MenuOption, ScenarioData Data)>>>
        {
            { 00, ExitProgram },
            { 01, ShowCreateMatch },
            { 02, ShowSelectMatch },
            { 08, ShowCreatePlayer },
            { 09, ShowMainMenu },
            {
                99,
                async data =>
                {
                    await data.Match.Get().Start();
                    return (-1, data);
                }
            },
        };

        public static async Task Main()
        {
            SetupScreen();

            var cluster = await CreateCluster((attempt, attemptsBeforeFailing) => WriteInfo($"Attempt {attempt} of {attemptsBeforeFailing} to start the game."));

            await cluster.Match(HandlerError, ShowScenario);
        }

        private static async Task ShowScenario(IClusterClient clusterClient)
        {
            (int MenuOption, ScenarioData Data) data = (
                08,
                new ScenarioData(
                    MenuOptions.Keys.ToList(),
                    Some(clusterClient),
                    player: None,
                    playerName: None,
                    match: None));

            while (data.MenuOption != -1)
            {
                ClearScreen();
                data = await MenuOptions[data.MenuOption](data.Data);
            }

            while (true)
            {
            }
        }

        private static Task HandlerError(Exception exception)
        {
            WriteError("Error starting game.");
            return CompletedTask;
        }

        private static Task<(int MenuOption, ScenarioData Data)> ExitProgram(ScenarioData data)
        {
            Exit(0);
            return null;
        }
    }
}
