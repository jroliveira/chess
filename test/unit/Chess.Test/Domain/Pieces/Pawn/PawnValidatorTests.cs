namespace Chess.Test.Domain.Pieces.Pawn
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Pawn;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Test.Domain.Pieces.Shared;

    using Xunit;

    using static Chess.Domain.Pieces.Pawn.Pawn;
    using static Chess.PieceColor;

    public sealed class PawnValidatorTests : ValidatorBaseTests
    {
        public PawnValidatorTests()
            : base(new PawnValidator(), new List<PieceBase>
            {
                CreatePawn("a3", WhitePiece, new Position[] { "a2" }),
                CreatePawn("b2", WhitePiece),
                CreatePawn("c4", WhitePiece, new Position[] { "c2" }),
                CreatePawn("d2", WhitePiece),
                CreatePawn("e2", WhitePiece),
                CreatePawn("f4", WhitePiece, new Position[] { "f2" }),
                CreatePawn("g2", WhitePiece),
                CreatePawn("h2", WhitePiece),
                CreatePawn("a7", BlackPiece),
                CreatePawn("b4", BlackPiece, new Position[] { "b7", "b5" }),
                CreatePawn("c7", BlackPiece),
                CreatePawn("d5", BlackPiece, new Position[] { "d7" }),
                CreatePawn("e7", BlackPiece),
                CreatePawn("f5", BlackPiece, new Position[] { "f7" }),
                CreatePawn("g6", BlackPiece, new Position[] { "g7" }),
                CreatePawn("h7", BlackPiece),
            })
        {
        }

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │   │   │   │   │   │   │   │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │*p*│ ↓ │*p*│ ↓ │*p*│ ↓ │ ↓ │*p*│ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │ ↓ │   │ ↓ │   │ ↓ │*p*│   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │ ↓ │   │*p*│   │*p*│   │   │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │*p*│ p │   │   │ p │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │ p │   │ ↑ │   │   │ ↑ │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │ ↑ │ p │ ↑ │ p │ p │ ↑ │ p │ p │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │   │   │   │   │   │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Theory]
        [InlineData("a3", "b4", true)]
        [InlineData("b2", "b3", true)]
        [InlineData("c4", "c5", true)]
        [InlineData("d2", "d4", true)]
        [InlineData("e2", "e4", true)]
        [InlineData("g2", "g3", true)]
        [InlineData("h2", "h4", true)]
        [InlineData("a7", "a5", true)]
        [InlineData("b4", "a3", true)]
        [InlineData("c7", "c5", true)]
        [InlineData("d5", "d4", true)]
        [InlineData("e7", "e5", true)]
        [InlineData("g6", "g5", true)]
        [InlineData("h7", "h6", true)]
        [InlineData("a3", "a5", false)]
        [InlineData("b2", "b4", false)]
        [InlineData("c4", "c3", false)]
        [InlineData("d2", "d5", false)]
        [InlineData("e2", "f4", false)]
        [InlineData("g2", "g5", false)]
        [InlineData("h2", "g3", false)]
        [InlineData("a7", "b7", false)]
        [InlineData("b4", "b2", false)]
        [InlineData("c7", "d6", false)]
        [InlineData("d5", "d6", false)]
        [InlineData("e7", "e4", false)]
        [InlineData("g6", "f5", false)]
        [InlineData("h7", "g7", false)]
        public void IsValid_GivenPositionAndNewPosition_ShouldReturn(string position, string newPosition, bool expected) => this.Validate(position, newPosition, expected);
    }
}
