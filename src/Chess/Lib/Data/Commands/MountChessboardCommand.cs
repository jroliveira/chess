namespace Chess.Lib.Data.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using Chess.Entities;
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

        public Type this[(IEnumerable<int> Ranks, IEnumerable<int> Files) key] => Config[key];

        public bool Contains(int rank, int file, out (IEnumerable<int>, IEnumerable<int>) key)
        {
            key = Config.Keys.FirstOrDefault(item => item.Ranks.Contains(rank) && item.Files.Contains(file));

            return Config.ContainsKey(key);
        }
    }

    internal class MountChessboardCommand
    {
        private static readonly ChessboardConfig Config = new ChessboardConfig();

        public virtual void Execute(Chessboard chessboard) => Observable
            .Range(0, 8)
            .Subscribe(r => Observable
                .Range(0, 8)
                .Subscribe(f =>
                {
                    if (!Config.Contains(r, f, out var key))
                    {
                        return;
                    }

                    var file = chessboard.Files.ElementAt(f);
                    var rank = chessboard.Ranks.ElementAt(r);
                    var position = new Position(file, rank);

                    PutPiece(Config[key], position, chessboard);
                }));

        private static void PutPiece(Type type, Position position, Chessboard chessboard)
        {
            var owner = Models.Owner.FirstPlayer;

            switch (position.Rank)
            {
                case '7':
                case '8':
                    owner = Models.Owner.SecondPlayer;
                    break;
            }

            var newPiece = Activator.CreateInstance(type, owner, position, chessboard) as Piece;

            chessboard.AddPiece(newPiece);
        }
    }
}
