namespace Chess.Test.Pieces
{
    using Chess.Entities.Pieces;
    using Chess.Lib.Extensions;

    using FluentAssertions;

    using Xunit;

    public class QueenTests : PieceTests
    {
        [Theory]
        [InlineData("a1", "♕")]
        [InlineData("a8", "♛")]
        public void ToString_GivenPosition_ShouldReturnPiece(string position, string expected)
        {
            string actual = new Queen(position.ToPosition(), default);

            actual.Should().Be(expected);
        }

        internal override Piece CreatePiece(string position) => new Queen(position.ToPosition(), default);
    }
}
