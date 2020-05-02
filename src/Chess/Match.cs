namespace Chess
{
    using System.Collections.Generic;

    public sealed class Match
    {
        private Match(
            Pieces pieces,
            IReadOnlyCollection<User> players,
            IReadOnlyCollection<User> spectators)
        {
            this.Pieces = pieces;
            this.Players = players;
            this.Spectators = spectators;
        }

        public Pieces Pieces { get; }

        public IReadOnlyCollection<User> Players { get; }

        public IReadOnlyCollection<User> Spectators { get; }

        public static Match CreateMatch(
            Pieces pieces,
            IReadOnlyCollection<User> players,
            IReadOnlyCollection<User> spectators) => new Match(pieces, players, spectators);
    }
}
