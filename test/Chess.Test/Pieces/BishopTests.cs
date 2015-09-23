using Chess.Pieces;
using Chess.Validations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Pieces
{
    [TestFixture]
    public class BishopTests
    {
        private Bishop _bishop;
        private Mock<Position> _positionStub;
        private Mock<Chessboard> _chessboardStub;
        private Mock<IValidator> _validatorMock;

        [SetUp]
        public void SetUp()
        {
            _positionStub = new Mock<Position>();
            _chessboardStub = new Mock<Chessboard>();
            _validatorMock = new Mock<IValidator>();

            _bishop = new Bishop(1, _positionStub.Object, _chessboardStub.Object, _validatorMock.Object);
        }

        [Test]
        public void Name_DeveRetornarNomeFormatado()
        {
            _bishop.Name.Should().Be("Bish");
        }

        [Test]
        public void Move_DadaNovaPosicao_DeveAlterarPosicao()
        {
            var newPositionStub = new Mock<Position>();
            newPositionStub.Setup(p => p.File).Returns('d');
            newPositionStub.Setup(p => p.Rank).Returns('7');

            _bishop.Move(newPositionStub.Object);

            _bishop.Position.ShouldBeEquivalentTo(newPositionStub.Object);
        }

        [Test]
        public void CanMove_DeveChamarValidatorUmaVez()
        {
            _bishop.CanMove(It.IsAny<Position>());

            _validatorMock.Verify(t => t.Validate(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Equals_DadaPecaNaPosicaoC3ENovaPecaNaPosicaoC3_DeveRetornarTrue()
        {
            _positionStub.Setup(p => p.File).Returns('c');
            _positionStub.Setup(p => p.Rank).Returns('3');

            var bishopStub = new Mock<Bishop>();
            bishopStub.Setup(p => p.Position).Returns(_positionStub.Object);

            var actual = _bishop.Equals(bishopStub.Object);
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

            var bishopStub = new Mock<Bishop>();
            bishopStub.Setup(p => p.Position).Returns(positionStub.Object);

            var actual = _bishop.Equals(bishopStub.Object);
            actual.Should().BeFalse();
        }
    }
}
