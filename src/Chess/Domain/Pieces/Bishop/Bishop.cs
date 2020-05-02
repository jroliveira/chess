namespace Chess.Domain.Pieces.Bishop
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;

    using static Chess.Domain.Pieces.Shared.PieceSymbols.Bishop;

    internal sealed class Bishop : PieceBase
    {
        private Bishop(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves)
            : base(
                position,
                color,
                logMoves,
                (White, Black),
                new BishopValidator())
        {
        }

        internal static Bishop CreateBishop(
            Position position,
            PieceColor color) => new Bishop(
                position,
                color,
                new List<Position>());

        internal static Bishop CreateBishop(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves) => new Bishop(
                position,
                color,
                logMoves);
    }
}
