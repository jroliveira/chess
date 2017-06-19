namespace Chess.Test.Validations.PawnValidations
{
    using Chess.Pieces;
    using Chess.Validations.PawnValidations;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class FileAndRankLimitValidateTests
    {
        private FileAndRankLimitValidate validate;
        private Mock<Chessboard> chessboardStub;
        private Mock<Pawn> pawnStub;

        [SetUp]
        public void Setup()
        {
            this.chessboardStub = new Mock<Chessboard>();
            this.chessboardStub.Setup(m => m.HasPiece(It.IsAny<Position>())).Returns(true);

            this.pawnStub = new Mock<Pawn>();
            this.pawnStub.Setup(p => p.Position).Returns(new Position('b', '7'));
            this.pawnStub.Setup(p => p.Chessboard).Returns(this.chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(this.pawnStub.Object);
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

            var actual = this.validate.IsValid(newPosition);

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
            this.pawnStub
                .Setup(p => p.Player)
                .Returns(player);

            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }

        [TestCase('a', '8')]
        [TestCase('c', '8')]
        [TestCase('a', '6')]
        [TestCase('c', '6')]
        public void IsValid_DadaUmaPosicaoValidaSemPeca_DeveRetornarTrue(char file, char rank)
        {
            this.chessboardStub
                .Setup(m => m.HasPiece(It.IsAny<Position>()))
                .Returns(false);

            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }
    }
}
