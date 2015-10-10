using Chess.Pieces;
using Chess.Validations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Pieces
{
    [TestFixture]
    public class KingTests
    {
        private King _king;
        private Mock<Position> _positionStub;
        private Mock<Chessboard> _chessboardStub;
        private Mock<IValidator> _validatorMock;

        [SetUp]
        public void SetUp()
        {
            _positionStub = new Mock<Position>();
            _chessboardStub = new Mock<Chessboard>();
            _validatorMock = new Mock<IValidator>();

            _king = new King(1, _positionStub.Object, _chessboardStub.Object, _validatorMock.Object);
        }

        [TestCase(1, "♔")]
        [TestCase(2, "♚")]
        public void Name_DadoJogador_DeveRetornarPeca(int player, string piece)
        {
            _king = new King(player, _positionStub.Object, _chessboardStub.Object, _validatorMock.Object);

            _king.Name.Should().Be(piece);
        }

        [Test]
        public void Move_DadaNovaPosicao_DeveAlterarPosicao()
        {
            var newPositionStub = new Mock<Position>();
            newPositionStub.Setup(p => p.File).Returns('d');
            newPositionStub.Setup(p => p.Rank).Returns('7');

            _king.Move(newPositionStub.Object);

            _king.Position.ShouldBeEquivalentTo(newPositionStub.Object);
        }

        [Test]
        public void CanMove_DeveChamarValidatorUmaVez()
        {
            _king.CanMove(It.IsAny<Position>());

            _validatorMock.Verify(t => t.Validate(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Equals_DadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3_DeveRetornarTrue()
        {
            _positionStub.Setup(p => p.File).Returns('c');
            _positionStub.Setup(p => p.Rank).Returns('3');
            _positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var kingStub = new Mock<King>();
            kingStub.Setup(p => p.Position).Returns(_positionStub.Object);

            var actual = _king.Equals(kingStub.Object);
            actual.Should().BeTrue();
        }

        [Test]
        public void Equals_DadaPecaNaPosicaoC2ENovaPecaNaPosicaoC3_DeveRetornarFalse()
        {
            _positionStub.Setup(p => p.File).Returns('c');
            _positionStub.Setup(p => p.Rank).Returns('2');

            var positionStub = new Mock<Position>();
            positionStub.Setup(p => p.File).Returns('c');
            positionStub.Setup(p => p.Rank).Returns('3');

            var kingStub = new Mock<King>();
            kingStub.Setup(p => p.Position).Returns(positionStub.Object);

            var actual = _king.Equals(kingStub.Object);
            actual.Should().BeFalse();
        }
    }
}