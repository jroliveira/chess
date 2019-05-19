namespace Chess.Client.Scenarios
{
    using System.Collections.Generic;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    internal sealed class ScenarioData
    {
        public ScenarioData(
            IReadOnlyCollection<int> menuOptions,
            Option<IClusterClient> clusterClient = default,
            Option<IPlayer> player = default,
            Option<string> playerName = default,
            Option<IMatch> match = default)
        {
            this.MenuOptions = menuOptions;
            this.ClusterClient = clusterClient;
            this.Player = player;
            this.PlayerName = playerName;
            this.Match = match;
        }

        public IReadOnlyCollection<int> MenuOptions { get; }

        internal Option<IClusterClient> ClusterClient { get; }

        internal Option<IPlayer> Player { get; }

        internal Option<string> PlayerName { get; }

        internal Option<IMatch> Match { get; }
    }
}
