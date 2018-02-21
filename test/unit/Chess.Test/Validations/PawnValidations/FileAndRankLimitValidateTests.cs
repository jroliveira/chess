namespace Chess.Test.Validations.PawnValidations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Validations.PawnValidations;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class FileAndRankLimitValidateTests
    {
        private readonly FileAndRankLimitValidate validate;
        private readonly Mock<Chessboard> chessboardStub;
        private readonly Mock<Pawn> pawnStub;

        public FileAndRankLimitValidateTests()
        {
            this.chessboardStub = new Mock<Chessboard>();
            this.chessboardStub.Setup(m => m.HasPiece(It.IsAny<Position>())).Returns(true);

            this.pawnStub = new Mock<Pawn>();
            this.pawnStub.Setup(p => p.Position).Returns(new Position('b', 7));
            this.pawnStub.Setup(p => p.Chessboard).Returns(this.chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(this.pawnStub.Object);
        }

        [Theory]
        [InlineData('a', 7)]
        [InlineData('b', 7)]
        [InlineData('c', 7)]
        [InlineData('d', 7)]
        [InlineData('a', 5)]
        [InlineData('c', 5)]
        [InlineData('b', 4)]
        public void IsValidDadaUmaPosicaoInvalidaDeveRetornarFalse(char file, uint rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData('a', 8, true)]
        [InlineData('b', 8, true)]
        [InlineData('c', 8, true)]
        [InlineData('a', 6, false)]
        [InlineData('b', 6, false)]
        [InlineData('c', 6, false)]
        [InlineData('b', 5, false)]
        public void IsValidDadaUmaPosicaoValidaComPecaDoPrimeiroJogadorDeveRetornarTrue(char file, uint rank, bool isWhite)
        {
            this.pawnStub
                .Setup(p => p.IsWhite)
                .Returns(isWhite);

            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }

        [Theory]
        [InlineData('a', 8)]
        [InlineData('c', 8)]
        [InlineData('a', 6)]
        [InlineData('c', 6)]
        public void IsValidDadaUmaPosicaoValidaSemPecaDeveRetornarTrue(char file, uint rank)
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
