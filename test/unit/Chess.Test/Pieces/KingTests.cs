namespace Chess.Test.Pieces
{
    using Chess.Entities.Pieces;
    using Chess.Lib.Extensions;

    using FluentAssertions;

    using Xunit;

    public class KingTests : PieceTests
    {
        [Theory]
        [InlineData("a1", true, "♔")]
        [InlineData("a8", false, "♚")]
        public void ToString_GivenPosition_ShouldReturnPiece(string position, bool playerUseWhitePiece, string expected)
        {
            string actual = new King(position.ToPosition(), playerUseWhitePiece, default);

            actual.Should().Be(expected);
        }

        internal override Piece CreatePiece(string position, bool playerUseWhitePiece) => new King(position.ToPosition(), playerUseWhitePiece, default);
    }
}
