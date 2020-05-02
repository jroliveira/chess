namespace Chess.Domain.Pieces.Pawn
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;

    using static Chess.Domain.Pieces.Shared.PieceSymbols.Pawn;

    internal sealed class Pawn : PieceBase
    {
        private Pawn(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves)
            : base(
                position,
                color,
                logMoves,
                (White, Black),
                new PawnValidator())
        {
        }

        internal static Pawn CreatePawn(
            Position position,
            PieceColor color) => new Pawn(
                position,
                color,
                new List<Position>());

        internal static Pawn CreatePawn(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves) => new Pawn(
                position,
                color,
                logMoves);
    }
}
