namespace Chess.Test.Domain.Pieces.Rook
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Rook;
    using Chess.Domain.Pieces.Shared;
    using Chess.Test.Domain.Pieces.Shared;

    using Xunit;

    using static Chess.Domain.Pieces.Rook.Rook;
    using static Chess.PieceColor;

    public sealed class RookValidatorTests : ValidatorBaseTests
    {
        public RookValidatorTests()
            : base(new RookValidator(), new List<PieceBase>
            {
                CreateRook("d5", WhitePiece),
            })
        {
        }

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │   │   │   │ ● │   │   │   │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │   │   │   │   │ ○ │ ○ │   │   │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │ ○ │ ○ │ ● │   │   │   │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │ ● │   │ ● │ R │ ● │   │   │ ● │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │   │   │ ● │   │   │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │   │   │ ○ │   │   │   │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │   │   │   │   │   │   │ ○ │   │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │ ● │   │   │   │   │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Theory]
        [InlineData("d5", "c5", true)]
        [InlineData("d5", "a5", true)]
        [InlineData("d5", "e5", true)]
        [InlineData("d5", "h5", true)]
        [InlineData("d5", "d6", true)]
        [InlineData("d5", "d8", true)]
        [InlineData("d5", "d4", true)]
        [InlineData("d5", "d1", true)]
        [InlineData("d5", "c6", false)]
        [InlineData("d5", "b6", false)]
        [InlineData("d5", "e7", false)]
        [InlineData("d5", "f7", false)]
        [InlineData("d5", "g2", false)]
        [InlineData("d5", "c3", false)]
        public void IsValid_GivenPositionAndNewPosition_ShouldReturn(string position, string newPosition, bool expected) => this.Validate(position, newPosition, expected);
    }
}
