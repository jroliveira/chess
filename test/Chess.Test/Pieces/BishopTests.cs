namespace Chess.Test.Pieces
{
    using Chess.Pieces;
    using Chess.Validations;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class BishopTests
    {
        private Bishop bishop;
        private Mock<Position> positionStub;
        private Mock<Chessboard> chessboardStub;
        private Mock<IValidator> validatorMock;

        [SetUp]
        public void SetUp()
        {
            this.positionStub = new Mock<Position>();
            this.chessboardStub = new Mock<Chessboard>();
            this.validatorMock = new Mock<IValidator>();

            this.bishop = new Bishop(1, this.positionStub.Object, this.chessboardStub.Object, this.validatorMock.Object);
        }

        [TestCase(1, "♗")]
        [TestCase(2, "♝")]
        public void Name_DadoJogador_DeveRetornarPeca(int player, string piece)
        {
            this.bishop = new Bishop(player, this.positionStub.Object, this.chessboardStub.Object, this.validatorMock.Object);

            this.bishop.Name.Should().Be(piece);
        }

        [Test]
        public void Move_DadaNovaPosicao_DeveAlterarPosicao()
        {
            var newPositionStub = new Mock<Position>();
            newPositionStub.Setup(p => p.File).Returns('d');
            newPositionStub.Setup(p => p.Rank).Returns('7');

            this.bishop.Move(newPositionStub.Object);

            this.bishop.Position.ShouldBeEquivalentTo(newPositionStub.Object);
        }

        [Test]
        public void CanMove_DeveChamarValidatorUmaVez()
        {
            this.bishop.CanMove(It.IsAny<Position>());

            this.validatorMock.Verify(t => t.Validate(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Equals_DadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3_DeveRetornarTrue()
        {
            this.positionStub.Setup(p => p.File).Returns('c');
            this.positionStub.Setup(p => p.Rank).Returns('3');
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var bishopStub = new Mock<Bishop>();
            bishopStub.Setup(p => p.Position).Returns(this.positionStub.Object);

            var actual = this.bishop.Equals(bishopStub.Object);
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

            var bishopStub = new Mock<Bishop>();
            bishopStub.Setup(p => p.Position).Returns(position.Object);

            var actual = this.bishop.Equals(bishopStub.Object);
            actual.Should().BeFalse();
        }
    }
}
