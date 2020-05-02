namespace Chess.Test.Domain.Pieces.King
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.King;
    using Chess.Domain.Pieces.Shared;
    using Chess.Test.Domain.Pieces.Shared;

    using Xunit;

    using static Chess.Domain.Pieces.King.King;
    using static Chess.PieceColor;

    public sealed class KingValidatorTests : ValidatorBaseTests
    {
        public KingValidatorTests()
            : base(new KingValidator(), new List<PieceBase>
            {
                CreateKing("f5", WhitePiece),
            })
        {
        }

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │   │   │   │   │   │ ○ │   │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │   │   │   │ ○ │   │   │   │ ○ │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │   │   │ ○ │ ● │ ● │ ● │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │   │   │ ○ │ ● │ K │ ● │ ○ │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │   │   │ ○ │ ● │ ● │ ● │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │   │   │   │   │ ○ │ ○ │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │   │   │   │   │   │   │   │ ○ │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │   │   │   │   │   │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Theory]
        [InlineData("f5", "e6", true)]
        [InlineData("f5", "f6", true)]
        [InlineData("f5", "g6", true)]
        [InlineData("f5", "g5", true)]
        [InlineData("f5", "g4", true)]
        [InlineData("f5", "f4", true)]
        [InlineData("f5", "e4", true)]
        [InlineData("f5", "e5", true)]
        [InlineData("f5", "d7", false)]
        [InlineData("f5", "d6", false)]
        [InlineData("f5", "f8", false)]
        [InlineData("f5", "h7", false)]
        [InlineData("f5", "h5", false)]
        [InlineData("f5", "h2", false)]
        [InlineData("f5", "f3", false)]
        [InlineData("f5", "e3", false)]
        [InlineData("f5", "d4", false)]
        [InlineData("f5", "d5", false)]
        public void IsValid_GivenPositionAndNewPosition_ShouldReturn(string position, string newPosition, bool expected) => this.Validate(position, newPosition, expected);
    }
}
