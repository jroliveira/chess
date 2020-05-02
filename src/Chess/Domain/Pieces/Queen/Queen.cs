namespace Chess.Domain.Pieces.Queen
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;

    using static Chess.Domain.Pieces.Shared.PieceSymbols.Queen;

    internal sealed class Queen : PieceBase
    {
        private Queen(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves)
            : base(
                position,
                color,
                logMoves,
                (White, Black),
                new QueenValidator())
        {
        }

        internal static Queen CreateQueen(
            Position position,
            PieceColor color) => new Queen(
                position,
                color,
                new List<Position>());

        internal static Queen CreateQueen(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves) => new Queen(
                position,
                color,
                logMoves);
    }
}
