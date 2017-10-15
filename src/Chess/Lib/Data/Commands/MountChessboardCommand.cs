namespace Chess.Lib.Data.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;

    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Models;

    using Piece = Chess.Entities.Pieces.Piece;

    internal class MountChessboardCommand
    {
        private readonly IDictionary<(IEnumerable<int> Ranks, IEnumerable<int> Files), Type> config;

        public MountChessboardCommand()
        {
            this.config = new Dictionary<(IEnumerable<int> Ranks, IEnumerable<int> Files), Type>
            {
                { (new [] { 0, 7 }, new [] { 0, 7 }), typeof(Rook) },
                { (new [] { 0, 7 }, new [] { 1, 6 }), typeof(Knight) },
                { (new [] { 0, 7 }, new [] { 2, 5 }), typeof(Bishop) },
                { (new [] { 0, 7 }, new [] { 3 }), typeof(King) },
                { (new [] { 0, 7 }, new [] { 4 }), typeof(Queen) },
                { (new [] { 1, 6 }, new [] { 0, 1, 2, 3, 4, 5, 6, 7 } ), typeof(Pawn) }
            };
        }

        public virtual void Execute(Chessboard chessboard)
        {
            Observable
                .Range(0, 8)
                .Subscribe(r => Observable
                    .Range(0, 8)
                    .Subscribe(f =>
                    {
                        var key = this.config.Keys.FirstOrDefault(item => item.Ranks.Contains(r) && item.Files.Contains(f));
                        if (this.config.ContainsKey(key))
                        {
                            var file = chessboard.Files.ElementAt(f);
                            var rank = chessboard.Ranks.ElementAt(r);
                            var position = new Position(file, rank);

                            PutPiece(this.config[key], position, chessboard);
                        }
                    }));
        }

        private static void PutPiece(Type type, Position position, Chessboard chessboard)
        {
            var owner = Owner.FirstPlayer;

            switch (position.Rank)
            {
                case '7':
                case '8':
                    owner = Owner.SecondPlayer;
                    break;
            }

            var newPiece = Activator.CreateInstance(type, owner, position, chessboard) as Piece;

            chessboard.AddPiece(newPiece);
        }
    }
}
