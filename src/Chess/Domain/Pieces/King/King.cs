namespace Chess.Domain.Pieces.King
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;

    using static Chess.Domain.Pieces.Shared.PieceSymbols.King;

    internal sealed class King : PieceBase
    {
        private King(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves)
            : base(
                position,
                color,
                logMoves,
                (White, Black),
                new KingValidator())
        {
        }

        internal static King CreateKing(
            Position position,
            PieceColor color) => new King(
                position,
                color,
                new List<Position>());

        internal static King CreateKing(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves) => new King(
                position,
                color,
                logMoves);
    }
}
