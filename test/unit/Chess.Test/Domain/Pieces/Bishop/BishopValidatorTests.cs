namespace Chess.Test.Domain.Pieces.Bishop
{
    using System.Collections.Generic;

    using Chess.Domain.Pieces.Bishop;
    using Chess.Domain.Pieces.Shared;
    using Chess.Test.Domain.Pieces.Shared;

    using Xunit;

    using static Chess.Domain.Pieces.Bishop.Bishop;
    using static Chess.PieceColor;

    public sealed class BishopValidatorTests : ValidatorBaseTests
    {
        public BishopValidatorTests()
            : base(new BishopValidator(), new List<PieceBase>
            {
                CreateBishop("d5", WhitePiece),
            })
        {
        }

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │ ● │   │   │   │   │   │ ● │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │   │   │   │ ○ │   │   │   │   │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │ ○ │ ● │   │ ● │ ○ │   │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │   │ ○ │ B │   │   │   │ ○ │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │ ○ │ ● │   │ ● │   │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │   │   │   │   │ ○ │   │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │ ● │   │   │   │   │   │   │   │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │ ○ │   │   │   │ ● │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Theory]
        [InlineData("d5", "c6", true)]
        [InlineData("d5", "a8", true)]
        [InlineData("d5", "e6", true)]
        [InlineData("d5", "g8", true)]
        [InlineData("d5", "c4", true)]
        [InlineData("d5", "a2", true)]
        [InlineData("d5", "e4", true)]
        [InlineData("d5", "h1", true)]
        [InlineData("d5", "c5", false)]
        [InlineData("d5", "b6", false)]
        [InlineData("d5", "d7", false)]
        [InlineData("d5", "f6", false)]
        [InlineData("d5", "h5", false)]
        [InlineData("d5", "e3", false)]
        [InlineData("d5", "d1", false)]
        [InlineData("d5", "b4", false)]
        public void Validate_GivenPositionAndNewPosition_ShouldReturn(string position, string newPosition, bool expected) => this.Validate(position, newPosition, expected);
    }
}
