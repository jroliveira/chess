namespace Chess.Lib.Data.Commands
{
    using System;
    using System.Linq;

    using Chess.Entities;
    using Chess.Entities.Pieces;

    using static System.Activator;
    using static System.Reactive.Linq.Observable;

    internal class MountChessboardCommand
    {
        private static readonly ChessboardConfig Config = new ChessboardConfig();

        internal virtual void Execute(Chessboard chessboard) => Range(0, 8).Subscribe(r => Range(0, 8).Subscribe(f =>
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
            var newPiece = CreateInstance(type, position, chessboard) as Piece;

            chessboard.AddPiece(newPiece);
        }
    }
}
