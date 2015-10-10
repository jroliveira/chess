using Chess.Pieces;
using Chess.Validations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Pieces
{
    [TestFixture]
    public class KnightTests
    {
        private Knight _knight;
        private Mock<Position> _positionStub;
        private Mock<Chessboard> _chessboardStub;
        private Mock<IValidator> _validatorMock;

        [SetUp]
        public void SetUp()
        {
            _positionStub = new Mock<Position>();
            _chessboardStub = new Mock<Chessboard>();
            _validatorMock = new Mock<IValidator>();

            _knight = new Knight(1, _positionStub.Object, _chessboardStub.Object, _validatorMock.Object);
        }

        [TestCase(1, "♘")]
        [TestCase(2, "♞")]
        public void Name_DadoJogador_DeveRetornarPeca(int player, string piece)
        {
            _knight = new Knight(player, _positionStub.Object, _chessboardStub.Object, _validatorMock.Object);

            _knight.Name.Should().Be(piece);
        }

        [Test]
        public void Move_DadaNovaPosicao_DeveAlterarPosicao()
        {
            var newPositionStub = new Mock<Position>();
            newPositionStub.Setup(p => p.File).Returns('d');
            newPositionStub.Setup(p => p.Rank).Returns('7');

            _knight.Move(newPositionStub.Object);

            _knight.Position.ShouldBeEquivalentTo(newPositionStub.Object);
        }

        [Test]
        public void CanMove_DeveChamarValidatorUmaVez()
        {
            _knight.CanMove(It.IsAny<Position>());

            _validatorMock.Verify(t => t.Validate(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Equals_DadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3_DeveRetornarTrue()
        {
            _positionStub.Setup(p => p.File).Returns('c');
            _positionStub.Setup(p => p.Rank).Returns('3');
            _positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var knightStub = new Mock<Knight>();
            knightStub.Setup(p => p.Position).Returns(_positionStub.Object);

            var actual = _knight.Equals(knightStub.Object);
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

            var knightStub = new Mock<Knight>();
            knightStub.Setup(p => p.Position).Returns(positionStub.Object);

            var actual = _knight.Equals(knightStub.Object);
            actual.Should().BeFalse();
        }
    }
}