namespace Chess.Test.Domain.Pieces.Knight
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Knight;
    using Chess.Domain.Pieces.Shared;
    using Chess.Test.Domain.Pieces.Shared;

    using Xunit;

    using static Chess.Domain.Pieces.Knight.Knight;
    using static Chess.PieceColor;

    public sealed class KnightValidatorTests : ValidatorBaseTests
    {
        public KnightValidatorTests()
            : base(new KnightValidator(), new List<PieceBase>
            {
                CreateKnight("d4", WhitePiece),
            })
        {
        }

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │   │   │   │   │   │   │   │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │   │   │   │   │ ○ │   │   │   │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │   │ ● │   │ ● │   │   │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │ ● │ ○ │ ○ │ ○ │ ● │   │   │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │ ○ │ ○ │ N │ ○ │ ○ │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │ ○ │ ● │ ○ │ ○ │ ○ │ ● │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │   │   │ ● │   │ ● │   │   │   │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │   │   │   │   │   │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Theory]
        [InlineData("d4", "b5", true)]
        [InlineData("d4", "c6", true)]
        [InlineData("d4", "e6", true)]
        [InlineData("d4", "f5", true)]
        [InlineData("d4", "f3", true)]
        [InlineData("d4", "e2", true)]
        [InlineData("d4", "c2", true)]
        [InlineData("d4", "b3", true)]
        [InlineData("d4", "c4", false)]
        [InlineData("d4", "c5", false)]
        [InlineData("d4", "d5", false)]
        [InlineData("d4", "e5", false)]
        [InlineData("d4", "e4", false)]
        [InlineData("d4", "e3", false)]
        [InlineData("d4", "d3", false)]
        [InlineData("d4", "c3", false)]
        [InlineData("d4", "b4", false)]
        [InlineData("d4", "e7", false)]
        [InlineData("d4", "f4", false)]
        [InlineData("d4", "f2", false)]
        [InlineData("d4", "a3", false)]
        public void IsValid_GivenPositionAndNewPosition_ShouldReturn(string position, string newPosition, bool expected) => this.Validate(position, newPosition, expected);
    }
}
