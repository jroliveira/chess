namespace Chess.Test.Validations.KingValidations
{
    using Chess.Pieces;
    using Chess.Validations.KingValidations;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class FileAndRankLimitValidateTests
    {
        private readonly FileAndRankLimitValidate validate;

        public FileAndRankLimitValidateTests()
        {
            var chessboardStub = new Mock<Chessboard>();
            chessboardStub.Setup(m => m.HasPiece(It.IsAny<Position>())).Returns(true);

            var kingStub = new Mock<King>();
            kingStub.Setup(p => p.Position).Returns(new Position('d', '5'));
            kingStub.Setup(p => p.Chessboard).Returns(chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(kingStub.Object);
        }

        [Theory]
        [InlineData('c', '7')]
        [InlineData('d', '7')]
        [InlineData('e', '7')]
        [InlineData('b', '5')]
        [InlineData('d', '5')]
        [InlineData('f', '5')]
        [InlineData('c', '3')]
        [InlineData('d', '3')]
        [InlineData('e', '3')]
        public void IsValid_DadaUmaPosicaoInvalida_DeveRetornarFalse(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData('c', '6')]
        [InlineData('d', '6')]
        [InlineData('e', '6')]
        [InlineData('c', '5')]
        [InlineData('e', '5')]
        [InlineData('c', '4')]
        [InlineData('d', '4')]
        [InlineData('e', '4')]
        public void IsValid_DadaUmaPosicaoValida_DeveRetornarTrue(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }
    }
}
