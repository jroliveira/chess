using Chess.Pieces;
using Chess.Validations.KingValidations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Validations.KingValidations
{
    [TestFixture]
    public class FileAndRankLimitValidateTests
    {
        private FileAndRankLimitValidate _validate;
        private Mock<Chessboard> _chessboardStub;
        private Mock<King> _kingStub;

        [SetUp]
        public void Setup()
        {
            _chessboardStub = new Mock<Chessboard>();
            _chessboardStub.Setup(m => m.HasPiece(It.IsAny<Position>())).Returns(true);

            _kingStub = new Mock<King>();
            _kingStub.Setup(p => p.Position).Returns(new Position('d', '5'));
            _kingStub.Setup(p => p.Chessboard).Returns(_chessboardStub.Object);

            _validate = new FileAndRankLimitValidate(_kingStub.Object);
        }

        [TestCase('c', '7')]
        [TestCase('d', '7')]
        [TestCase('e', '7')]
        [TestCase('b', '5')]
        [TestCase('d', '5')]
        [TestCase('f', '5')]
        [TestCase('c', '3')]
        [TestCase('d', '3')]
        [TestCase('e', '3')]
        public void IsValid_DadaUmaPosicaoInvalida_DeveRetornarFalse(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = _validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [TestCase('c', '6')]
        [TestCase('d', '6')]
        [TestCase('e', '6')]
        [TestCase('c', '5')]
        [TestCase('e', '5')]
        [TestCase('c', '4')]
        [TestCase('d', '4')]
        [TestCase('e', '4')]
        public void IsValid_DadaUmaPosicaoValida_DeveRetornarTrue(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = _validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }
    }
}
