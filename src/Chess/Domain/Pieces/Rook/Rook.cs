namespace Chess.Domain.Pieces.Rook
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;

    using static Chess.Domain.Pieces.Shared.PieceSymbols.Rook;

    internal sealed class Rook : PieceBase
    {
        private Rook(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves)
            : base(
                position,
                color,
                logMoves,
                (White, Black),
                new RookValidator())
        {
        }

        internal static Rook CreateRook(
            Position position,
            PieceColor color) => new Rook(
                position,
                color,
                new List<Position>());

        internal static Rook CreateRook(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves) => new Rook(
                position,
                color,
                logMoves);
    }
}
