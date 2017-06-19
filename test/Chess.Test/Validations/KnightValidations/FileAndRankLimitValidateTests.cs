namespace Chess.Test.Validations.KnightValidations
{
    using Chess.Pieces;
    using Chess.Validations.KnightValidations;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class FileAndRankLimitValidateTests
    {
        private FileAndRankLimitValidate validate;
        private Mock<Chessboard> chessboardStub;
        private Mock<Knight> knightStub;

        [SetUp]
        public void Setup()
        {
            this.chessboardStub = new Mock<Chessboard>();
            this.chessboardStub.Setup(m => m.HasPiece(It.IsAny<Position>())).Returns(true);

            this.knightStub = new Mock<Knight>();
            this.knightStub.Setup(p => p.Position).Returns(new Position('d', '5'));
            this.knightStub.Setup(p => p.Chessboard).Returns(this.chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(this.knightStub.Object);
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

            var actual = this.validate.IsValid(newPosition);

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

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }
    }
}
