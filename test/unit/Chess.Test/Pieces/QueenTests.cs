namespace Chess.Test.Pieces
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Models;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class QueenTests
    {
        private readonly Mock<Position> positionStub;
        private readonly Mock<Chessboard> chessboardStub;
        private Queen queen;

        public QueenTests()
        {
            this.positionStub = new Mock<Position>();
            this.chessboardStub = new Mock<Chessboard>();

            this.queen = new Queen(Owner.FirstPlayer, this.positionStub.Object, this.chessboardStub.Object);
        }

        [Theory]
        [InlineData(Owner.FirstPlayer, "♕")]
        [InlineData(Owner.SecondPlayer, "♛")]
        public void NameDadoJogadorDeveRetornarPeca(Owner owner, string piece)
        {
            this.queen = new Queen(owner, this.positionStub.Object, this.chessboardStub.Object);

            this.queen.Name.Should().Be(piece);
        }

        [Fact]
        public void EqualsDadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3DeveRetornarTrue()
        {
            this.positionStub.Setup(p => p.File).Returns('c');
            this.positionStub.Setup(p => p.Rank).Returns('3');
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var queenStub = new Mock<Queen>();
            queenStub.Setup(p => p.Position).Returns(this.positionStub.Object);

            var actual = this.queen.Equals(queenStub.Object);
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

            var queenStub = new Mock<Queen>();
            queenStub.Setup(p => p.Position).Returns(position.Object);

            var actual = this.queen.Equals(queenStub.Object);
            actual.Should().BeFalse();
        }
    }
}