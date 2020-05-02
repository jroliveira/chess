namespace Chess.Test.Domain.Chessboard
{
    using System;

    using Chess.Domain.Pieces.Bishop;
    using Chess.Domain.Pieces.King;
    using Chess.Domain.Pieces.Knight;
    using Chess.Domain.Pieces.Pawn;
    using Chess.Domain.Pieces.Queen;
    using Chess.Domain.Pieces.Rook;

    using FluentAssertions;

    using Xunit;

    using static Chess.Domain.Chessboard.InitialChessboardSetup;

    public sealed class InitialChessboardSetupTests
    {
        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │   │   │   │   │   │   │   │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │   │   │   │   │   │   │   │   │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │   │   │ ? │   │   │   │ ? │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │   │ ? │   │   │   │ ? │   │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │ ? │   │   │   │ ? │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │ ? │   │   │   │ ? │   │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │   │   │   │   │   │   │   │   │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │   │   │   │   │   │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Theory]
        [InlineData("a3")]
        [InlineData("b4")]
        [InlineData("c5")]
        [InlineData("d6")]
        [InlineData("e3")]
        [InlineData("f4")]
        [InlineData("g5")]
        [InlineData("h6")]
        public void GetPieceType_GivenPosition_ShouldReturnIsDefinedFalse(string position)
        {
            var actual = GetPieceType(position);

            actual.IsDefined.Should().BeFalse();
        }

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │ R │ N │ B │ Q │ K │ B │ N │ R │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │ p │ p │ p │ p │ p │ p │ p │ p │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │   │   │   │   │   │   │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │   │   │   │   │   │   │   │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │   │   │   │   │   │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │   │   │   │   │   │   │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │ p │ p │ p │ p │ p │ p │ p │ p │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │ R │ N │ B │ Q │ K │ B │ N │ R │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Theory]
        [InlineData("a1", typeof(Rook))]
        [InlineData("b1", typeof(Knight))]
        [InlineData("c1", typeof(Bishop))]
        [InlineData("d1", typeof(Queen))]
        [InlineData("e1", typeof(King))]
        [InlineData("f1", typeof(Bishop))]
        [InlineData("g1", typeof(Knight))]
        [InlineData("h1", typeof(Rook))]
        [InlineData("a2", typeof(Pawn))]
        [InlineData("b2", typeof(Pawn))]
        [InlineData("c2", typeof(Pawn))]
        [InlineData("d2", typeof(Pawn))]
        [InlineData("e2", typeof(Pawn))]
        [InlineData("f2", typeof(Pawn))]
        [InlineData("g2", typeof(Pawn))]
        [InlineData("h2", typeof(Pawn))]
        [InlineData("a7", typeof(Pawn))]
        [InlineData("b7", typeof(Pawn))]
        [InlineData("c7", typeof(Pawn))]
        [InlineData("d7", typeof(Pawn))]
        [InlineData("e7", typeof(Pawn))]
        [InlineData("f7", typeof(Pawn))]
        [InlineData("g7", typeof(Pawn))]
        [InlineData("h7", typeof(Pawn))]
        [InlineData("a8", typeof(Rook))]
        [InlineData("b8", typeof(Knight))]
        [InlineData("c8", typeof(Bishop))]
        [InlineData("d8", typeof(Queen))]
        [InlineData("e8", typeof(King))]
        [InlineData("f8", typeof(Bishop))]
        [InlineData("g8", typeof(Knight))]
        [InlineData("h8", typeof(Rook))]
        public void GetPieceType_GivenPosition_ShouldReturnPiece(string position, Type expectedType)
        {
            var actual = GetPieceType(position);

            actual.Get().Should().Be(expectedType);
        }
    }
}
