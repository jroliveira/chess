namespace Chess.Test.Pieces
{
    using Chess.Entities.Pieces;

    using FluentAssertions;

    using Xunit;

    public abstract class PieceTests
    {
        internal abstract Piece CreatePiece(string position);

        [Fact]
        public void Equals_GivenPieceInPositionC3AndNewPieceInPositionC3_ShouldReturnTrue()
        {
            var piece1 = this.CreatePiece("c3");
            var piece2 = this.CreatePiece("c3");

            var actual = piece1.Equals(piece2);

            actual.Should().BeTrue();
        }

        [Fact]
        public void Equals_GivenPieceInPositionC2AndNewPieceInPositionC3_ShouldReturnFalse()
        {
            var piece1 = this.CreatePiece("c2");
            var piece2 = this.CreatePiece("c3");

            var actual = piece1.Equals(piece2);

            actual.Should().BeFalse();
        }
    }
}
