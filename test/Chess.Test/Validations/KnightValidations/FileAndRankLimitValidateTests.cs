using Chess.Pieces;
using Chess.Validations.KnightValidations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Validations.KnightValidations
{
    [TestFixture]
    public class FileAndRankLimitValidateTests
    {
        private FileAndRankLimitValidate _validate;
        private Mock<Chessboard> _chessboardStub;
        private Mock<Knight> _knightStub;

        [SetUp]
        public void Setup()
        {
            _chessboardStub = new Mock<Chessboard>();
            _chessboardStub.Setup(m => m.HasPiece(It.IsAny<Position>())).Returns(true);

            _knightStub = new Mock<Knight>();
            _knightStub.Setup(p => p.Position).Returns(new Position('d', '5'));
            _knightStub.Setup(p => p.Chessboard).Returns(_chessboardStub.Object);

            _validate = new FileAndRankLimitValidate(_knightStub.Object);
        }

        [TestCase('b', '7')]
        [TestCase('d', '7')]
        [TestCase('f', '7')]
        [TestCase('c', '6')]
        [TestCase('d', '6')]
        [TestCase('e', '6')]
        [TestCase('b', '5')]
        [TestCase('c', '5')]
        [TestCase('e', '5')]
        [TestCase('f', '5')]
        [TestCase('c', '4')]
        [TestCase('d', '4')]
        [TestCase('e', '4')]
        [TestCase('b', '3')]
        [TestCase('d', '3')]
        [TestCase('f', '3')]
        public void IsValid_DadaUmaPosicaoInvalida_DeveRetornarFalse(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = _validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [TestCase('c', '7')]
        [TestCase('e', '7')]
        [TestCase('b', '6')]
        [TestCase('f', '6')]
        [TestCase('b', '4')]
        [TestCase('f', '4')]
        [TestCase('c', '3')]
        [TestCase('e', '3')]
        public void IsValid_DadaUmaPosicaoValida_DeveRetornarTrue(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = _validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }
    }
}
