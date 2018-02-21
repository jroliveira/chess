namespace Chess.Test.Pieces
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using FluentAssertions;
    using Moq;
    using Xunit;
    using Chessboard = Chess.Entities.Chessboard;

    public class KnightTests
    {
        private readonly Mock<Position> positionStub;
        private readonly Mock<Chessboard> chessboardStub;
        private Knight knight;

        public KnightTests()
        {
            this.positionStub = new Mock<Position>();
            this.chessboardStub = new Mock<Chessboard>();

            this.knight = new Knight(Models.Owner.FirstPlayer, this.positionStub.Object, this.chessboardStub.Object);
        }

        [Theory]
        [InlineData(Models.Owner.FirstPlayer, "♘")]
        [InlineData(Models.Owner.SecondPlayer, "♞")]
        public void NameDadoJogadorDeveRetornarPeca(Models.Owner owner, string piece)
        {
            this.knight = new Knight(owner, this.positionStub.Object, this.chessboardStub.Object);

            this.knight.Name.Should().Be(piece);
        }

        [Fact]
        public void EqualsDadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3DeveRetornarTrue()
        {
            this.positionStub.Setup(p => p.File).Returns('c');
            this.positionStub.Setup(p => p.Rank).Returns('3');
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var knightStub = new Mock<Knight>();
            knightStub.Setup(p => p.Position).Returns(this.positionStub.Object);

            var actual = this.knight.Equals(knightStub.Object);
            actual.Should().BeTrue();
        }

        [Fact]
        public void EqualsDadaPecaNaPosicaoC2ENovaPecaNaPosicaoC3DeveRetornarFalse()
        {
            this.positionStub.Setup(p => p.File).Returns('c');
            this.positionStub.Setup(p => p.Rank).Returns('2');

            var position = new Mock<Position>();
            position.Setup(p => p.File).Returns('c');
            position.Setup(p => p.Rank).Returns('3');

            var knightStub = new Mock<Knight>();
            knightStub.Setup(p => p.Position).Returns(position.Object);

            var actual = this.knight.Equals(knightStub.Object);
            actual.Should().BeFalse();
        }
    }
}