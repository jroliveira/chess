namespace Chess.Domain.Pieces.Knight
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;

    using static Chess.Domain.Pieces.Shared.PieceSymbols.Knight;

    internal sealed class Knight : PieceBase
    {
        private Knight(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves)
            : base(
                position,
                color,
                logMoves,
                (White, Black),
                new KnightValidator())
        {
        }

        internal static Knight CreateKnight(
            Position position,
            PieceColor color) => new Knight(
                position,
                color,
                new List<Position>());

        internal static Knight CreateKnight(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves) => new Knight(
                position,
                color,
                logMoves);
    }
}
