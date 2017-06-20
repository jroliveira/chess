namespace Chess.Test.Validations.BishopValidations
{
    using Chess.Pieces;
    using Chess.Validations.BishopValidations;

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

            var bishopStub = new Mock<Bishop>();
            bishopStub.Setup(p => p.Position).Returns(new Position('d', '5'));
            bishopStub.Setup(p => p.Chessboard).Returns(chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(bishopStub.Object);
        }

        [Theory]
        [InlineData('d', '7')]
        [InlineData('d', '6')]
        [InlineData('b', '5')]
        [InlineData('c', '5')]
        [InlineData('d', '5')]
        [InlineData('e', '5')]
        [InlineData('f', '5')]
        [InlineData('d', '4')]
        [InlineData('d', '3')]
        [InlineData('e', '3')]
        public void IsValid_DadaUmaPosicaoInvalida_DeveRetornarFalse(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData('b', '7')]
        [InlineData('f', '7')]
        [InlineData('c', '6')]
        [InlineData('e', '6')]
        [InlineData('c', '4')]
        [InlineData('e', '4')]
        [InlineData('b', '3')]
        [InlineData('f', '3')]
        public void IsValid_DadaUmaPosicaoValida_DeveRetornarTrue(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }
    }
}
