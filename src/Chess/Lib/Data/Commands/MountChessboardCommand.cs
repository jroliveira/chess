namespace Chess.Lib.Data.Commands
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using Chess.Entities;
    using Chess.Entities.Pieces;

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
