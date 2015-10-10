namespace Chess.Test.Pieces
{
    using Chess.Pieces;
    using Chess.Validations;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class KnightTests
    {
        private readonly Mock<Position> positionStub;
        private readonly Mock<Chessboard> chessboardStub;
        private readonly Mock<IValidator> validatorMock;
        private Knight knight;

        public KnightTests()
        {
            this.positionStub = new Mock<Position>();
            this.chessboardStub = new Mock<Chessboard>();
            this.validatorMock = new Mock<IValidator>();

            this.knight = new Knight(1, this.positionStub.Object, this.chessboardStub.Object, this.validatorMock.Object);
        }

        [Theory]
        [InlineData(1, "♘")]
        [InlineData(2, "♞")]
        public void Name_DadoJogador_DeveRetornarPeca(int player, string piece)
        {
            this.knight = new Knight(player, this.positionStub.Object, this.chessboardStub.Object, this.validatorMock.Object);

            this.knight.Name.Should().Be(piece);
        }

        [Fact]
        public void Move_DadaNovaPosicao_DeveAlterarPosicao()
        {
            var newPositionStub = new Mock<Position>();
            newPositionStub.Setup(p => p.File).Returns('d');
            newPositionStub.Setup(p => p.Rank).Returns('7');

            this.knight.Move(newPositionStub.Object);

            this.knight.Position.ShouldBeEquivalentTo(newPositionStub.Object);
        }

        [Fact]
        public void CanMove_DeveChamarValidatorUmaVez()
        {
            this.knight.CanMove(It.IsAny<Position>());

            this.validatorMock.Verify(t => t.Validate(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void Equals_DadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3_DeveRetornarTrue()
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
        public void Equals_DadaPecaNaPosicaoC2ENovaPecaNaPosicaoC3_DeveRetornarFalse()
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