namespace Chess.Test.Validations.KingValidations
{
    using Chess.Pieces;
    using Chess.Validations.KingValidations;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class FileAndRankLimitValidateTests
    {
        private FileAndRankLimitValidate validate;
        private Mock<Chessboard> chessboardStub;
        private Mock<King> kingStub;

        [SetUp]
        public void Setup()
        {
            this.chessboardStub = new Mock<Chessboard>();
            this.chessboardStub.Setup(m => m.HasPiece(It.IsAny<Position>())).Returns(true);

            this.kingStub = new Mock<King>();
            this.kingStub.Setup(p => p.Position).Returns(new Position('d', '5'));
            this.kingStub.Setup(p => p.Chessboard).Returns(this.chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(this.kingStub.Object);
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

            var actual = this.validate.IsValid(newPosition);

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

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }
    }
}
