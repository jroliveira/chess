using Chess.Pieces;
using Chess.Validations.PawnValidations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Validations.PawnValidations
{
    [TestFixture]
    public class FileAndRankLimitValidateTests
    {
        private FileAndRankLimitValidate _validate;
        private Mock<Chessboard> _chessboardStub;
        private Mock<Pawn> _pawnStub;

        [SetUp]
        public void Setup()
        {
            _chessboardStub = new Mock<Chessboard>();
            _chessboardStub.Setup(m => m.HasPiece(It.IsAny<Position>())).Returns(true);

            _pawnStub = new Mock<Pawn>();
            _pawnStub.Setup(p => p.Position).Returns(new Position('b', '7'));
            _pawnStub.Setup(p => p.Chessboard).Returns(_chessboardStub.Object);

            _validate = new FileAndRankLimitValidate(_pawnStub.Object);
        }

        [TestCase('a', '7')]
        [TestCase('b', '7')]
        [TestCase('c', '7')]
        [TestCase('d', '7')]
        [TestCase('a', '5')]
        [TestCase('c', '5')]
        [TestCase('b', '4')]
        public void IsValid_DadaUmaPosicaoInvalida_DeveRetornarFalse(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = _validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [TestCase('a', '8', 1)]
        [TestCase('b', '8', 1)]
        [TestCase('c', '8', 1)]
        [TestCase('a', '6', 2)]
        [TestCase('b', '6', 2)]
        [TestCase('c', '6', 2)]
        [TestCase('b', '5', 2)]
        public void IsValid_DadaUmaPosicaoValidaComPecaDoPrimeiroJogador_DeveRetornarTrue(char file, char rank, int player)
        {
            _pawnStub
                .Setup(p => p.Player)
                .Returns(player);

            var newPosition = new Position(file, rank);

            var actual = _validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }

        [TestCase('a', '8')]
        [TestCase('c', '8')]
        [TestCase('a', '6')]
        [TestCase('c', '6')]
        public void IsValid_DadaUmaPosicaoValidaSemPeca_DeveRetornarTrue(char file, char rank)
        {
            _chessboardStub
                .Setup(m => m.HasPiece(It.IsAny<Position>()))
                .Returns(false);

            var newPosition = new Position(file, rank);

            var actual = _validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }
    }
}
