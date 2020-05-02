namespace Chess.Orleans.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chess.Infra.Monad;
    using Chess.Orleans.ConsoleApp.Scenarios.CreateMatch;
    using Chess.Orleans.ConsoleApp.Scenarios.CreateUser;
    using Chess.Orleans.ConsoleApp.Scenarios.MainMenu;
    using Chess.Orleans.ConsoleApp.Scenarios.Match;
    using Chess.Orleans.ConsoleApp.Scenarios.SelectMatch;
    using Chess.Orleans.ConsoleApp.Scenarios.SelectPieceColor;
    using Chess.Orleans.Contract;

    using global::Orleans;

    using static System.Environment;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.GameMenuItem;

    internal sealed class GameData
    {
        private static readonly IReadOnlyDictionary<GameMenuItem, Func<GameData, Task<GameData>>> GameOptions = new Dictionary<GameMenuItem, Func<GameData, Task<GameData>>>
        {
            {
                ExitGame, _ =>
                {
                    Exit(0);
                    return default;
                }
            },
            { MainMenu, new MainMenuScenario().ShowScenario },
            { CreateMatch, new CreateMatchScenario().ShowScenario },
            { SelectMatch, new SelectMatchScenario().ShowScenario },
            { CreateUser, new CreateUserScenario().ShowScenario },
            { SelectPiece, new SelectPieceColorScenario().ShowScenario },
            { PlayMatch, new MatchScenario().ShowScenario },
        };

        private GameData(
            GameMenuItem gameMenuItem,
            Option<IClusterClient> clusterClientOption,
            Option<IUserGrain> userGrainOption,
            Option<string> userNameOption,
            Option<PieceColor> playingWithOption,
            Option<IMatchGrain> matchGrainOption,
            Option<Match> matchOption)
        {
            this.GameMenuItem = gameMenuItem;
            this.ClusterClientOption = clusterClientOption;
            this.UserGrainOption = userGrainOption;
            this.UserNameOption = userNameOption;
            this.PlayingWithOption = playingWithOption;
            this.MatchGrainOption = matchGrainOption;
            this.MatchOption = matchOption;
        }

        internal GameMenuItem GameMenuItem { get; }

        internal Option<IClusterClient> ClusterClientOption { get; }

        internal Option<IUserGrain> UserGrainOption { get; }

        internal Option<string> UserNameOption { get; }

        internal Option<PieceColor> PlayingWithOption { get; }

        internal Option<IMatchGrain> MatchGrainOption { get; }

        internal Option<Match> MatchOption { get; }

        public static implicit operator GameData(Match match) => new GameData(
            Default,
            clusterClientOption: None(),
            userGrainOption: None(),
            userNameOption: None(),
            playingWithOption: None(),
            matchGrainOption: None(),
            match);

        internal static GameData CreateGameData() => new GameData(
            Default,
            clusterClientOption: None(),
            userGrainOption: None(),
            userNameOption: None(),
            playingWithOption: None(),
            matchGrainOption: None(),
            matchOption: None());

        internal Task<GameData> ExecuteOption() => GameOptions[this.GameMenuItem](this);

        internal GameData SetGameOption(int gameMenuItem) => this.SetGameMenuItem((GameMenuItem)gameMenuItem);

        internal GameData SetGameMenuItem(GameMenuItem gameMenuItem) => new GameData(
            gameMenuItem,
            this.ClusterClientOption,
            this.UserGrainOption,
            this.UserNameOption,
            this.PlayingWithOption,
            this.MatchGrainOption,
            this.MatchOption);

        internal GameData SetClusterClient(Option<IClusterClient> clusterClientOption) => new GameData(
            this.GameMenuItem,
            clusterClientOption,
            this.UserGrainOption,
            this.UserNameOption,
            this.PlayingWithOption,
            this.MatchGrainOption,
            this.MatchOption);

        internal GameData SetUser(Option<IUserGrain> userGrainOption, Option<string> userNameOption) => new GameData(
            SelectPiece,
            this.ClusterClientOption,
            userGrainOption,
            userNameOption,
            this.PlayingWithOption,
            this.MatchGrainOption,
            this.MatchOption);

        internal GameData SetMatch(Option<IMatchGrain> matchGrainOption) => new GameData(
            CreateUser,
            this.ClusterClientOption,
            this.UserGrainOption,
            this.UserNameOption,
            this.PlayingWithOption,
            matchGrainOption,
            this.MatchOption);

        internal GameData SetPieceColor(Option<PieceColor> playingWithOption, Option<Match> matchOption) => new GameData(
            PlayMatch,
            this.ClusterClientOption,
            this.UserGrainOption,
            this.UserNameOption,
            playingWithOption,
            this.MatchGrainOption,
            matchOption);
    }
}
