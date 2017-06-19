namespace Chess.Test.Pieces
{
    using Chess.Pieces;
    using Chess.Validations;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class QueenTests
    {
        private Queen queen;
        private Mock<Position> positionStub;
        private Mock<Chessboard> chessboardStub;
        private Mock<IValidator> validatorMock;

        [SetUp]
        public void SetUp()
        {
            this.positionStub = new Mock<Position>();
            this.chessboardStub = new Mock<Chessboard>();
            this.validatorMock = new Mock<IValidator>();

            this.queen = new Queen(1, this.positionStub.Object, this.chessboardStub.Object, this.validatorMock.Object);
        }

        [TestCase(1, "♕")]
        [TestCase(2, "♛")]
        public void Name_DadoJogador_DeveRetornarPeca(int player, string piece)
        {
            this.queen = new Queen(player, this.positionStub.Object, this.chessboardStub.Object, this.validatorMock.Object);

            this.queen.Name.Should().Be(piece);
        }

        [Test]
        public void Move_DadaNovaPosicao_DeveAlterarPosicao()
        {
            var newPositionStub = new Mock<Position>();
            newPositionStub.Setup(p => p.File).Returns('d');
            newPositionStub.Setup(p => p.Rank).Returns('7');

            this.queen.Move(newPositionStub.Object);

            this.queen.Position.ShouldBeEquivalentTo(newPositionStub.Object);
        }

        [Test]
        public void CanMove_DeveChamarValidatorUmaVez()
        {
            this.queen.CanMove(It.IsAny<Position>());

            this.validatorMock.Verify(t => t.Validate(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Equals_DadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3_DeveRetornarTrue()
        {
            this.positionStub.Setup(p => p.File).Returns('c');
            this.positionStub.Setup(p => p.Rank).Returns('3');
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var queenStub = new Mock<Queen>();
            queenStub.Setup(p => p.Position).Returns(this.positionStub.Object);

            var actual = this.queen.Equals(queenStub.Object);
            actual.Should().BeTrue();
        }

        [Test]
        public void Equals_DadaPecaNaPosicaoC2ENovaPecaNaPosicaoC3_DeveRetornarFalse()
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