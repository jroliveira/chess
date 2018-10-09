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
            this.pawnStub.Setup(p => p.Position).Returns(new Position('b', '7'));
            this.pawnStub.Setup(p => p.Chessboard).Returns(this.chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(this.pawnStub.Object);
        }

        [Theory]
        [InlineData('a', '7')]
        [InlineData('b', '7')]
        [InlineData('c', '7')]
        [InlineData('d', '7')]
        [InlineData('a', '5')]
        [InlineData('c', '5')]
        [InlineData('b', '4')]
        public void IsValidDadaUmaPosicaoInvalidaDeveRetornarFalse(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData('a', '8', Models.Owner.FirstPlayer)]
        [InlineData('b', '8', Models.Owner.FirstPlayer)]
        [InlineData('c', '8', Models.Owner.FirstPlayer)]
        [InlineData('a', '6', Models.Owner.SecondPlayer)]
        [InlineData('b', '6', Models.Owner.SecondPlayer)]
        [InlineData('c', '6', Models.Owner.SecondPlayer)]
        [InlineData('b', '5', Models.Owner.SecondPlayer)]
        public void IsValidDadaUmaPosicaoValidaComPecaDoPrimeiroJogadorDeveRetornarTrue(char file, char rank, Models.Owner owner)
        {
            this.pawnStub
                .Setup(p => p.Owner)
                .Returns(owner);

            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }

        [Theory]
        [InlineData('a', '8')]
        [InlineData('c', '8')]
        [InlineData('a', '6')]
        [InlineData('c', '6')]
        public void IsValidDadaUmaPosicaoValidaSemPecaDeveRetornarTrue(char file, char rank)
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
