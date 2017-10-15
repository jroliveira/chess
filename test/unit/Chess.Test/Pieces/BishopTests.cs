namespace Chess.Test.Pieces
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Models;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class BishopTests
    {
        private readonly Mock<Position> positionStub;
        private readonly Mock<Chessboard> chessboardStub;
        private Bishop bishop;

        public BishopTests()
        {
            this.positionStub = new Mock<Position>();
            this.chessboardStub = new Mock<Chessboard>();

            this.bishop = new Bishop(Owner.FirstPlayer, this.positionStub.Object, this.chessboardStub.Object);
        }

        [Theory]
        [InlineData(Owner.FirstPlayer, "♗")]
        [InlineData(Owner.SecondPlayer, "♝")]
        public void NameDadoJogadorDeveRetornarPeca(Owner owner, string piece)
        {
            this.bishop = new Bishop(owner, this.positionStub.Object, this.chessboardStub.Object);

            this.bishop.Name.Should().Be(piece);
        }

        [Fact]
        public void EqualsDadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3DeveRetornarTrue()
        {
            this.positionStub.Setup(p => p.File).Returns('c');
            this.positionStub.Setup(p => p.Rank).Returns('3');
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var bishopStub = new Mock<Bishop>();
            bishopStub.Setup(p => p.Position).Returns(this.positionStub.Object);

            var actual = this.bishop.Equals(bishopStub.Object);
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

            var bishopStub = new Mock<Bishop>();
            bishopStub.Setup(p => p.Position).Returns(position.Object);

            var actual = this.bishop.Equals(bishopStub.Object);
            actual.Should().BeFalse();
        }
    }
}
