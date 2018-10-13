namespace Chess.Lib.Data.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Entities.Pieces;

    internal sealed class ChessboardConfig
    {
        private static readonly IDictionary<(IEnumerable<int> Ranks, IEnumerable<int> Files), Type> Config = new Dictionary<(IEnumerable<int> Ranks, IEnumerable<int> Files), Type>
        {
            { (new[] { 0, 7 }, new[] { 0, 7 }), typeof(Rook) },
            { (new[] { 0, 7 }, new[] { 1, 6 }), typeof(Knight) },
            { (new[] { 0, 7 }, new[] { 2, 5 }), typeof(Bishop) },
            { (new[] { 0, 7 }, new[] { 3 }), typeof(King) },
            { (new[] { 0, 7 }, new[] { 4 }), typeof(Queen) },
            { (new[] { 1, 6 }, new[] { 0, 1, 2, 3, 4, 5, 6, 7, }), typeof(Pawn) },
        };

        internal Type this[(IEnumerable<int> Ranks, IEnumerable<int> Files) key] => Config[key];

        internal bool Contains(int rank, int file, out (IEnumerable<int>, IEnumerable<int>) key)
        {
            key = Config.Keys.FirstOrDefault(item => item.Ranks.Contains(rank) && item.Files.Contains(file));

            return Config.ContainsKey(key);
        }
    }
}
