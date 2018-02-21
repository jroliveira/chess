namespace Chess.Test.Validations.KnightValidations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Validations.KnightValidations;
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

            var knightStub = new Mock<Knight>();
            knightStub.Setup(p => p.Position).Returns(new Position('d', '5'));
            knightStub.Setup(p => p.Chessboard).Returns(chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(knightStub.Object);
        }

        [Theory]
        [InlineData('b', '7')]
        [InlineData('d', '7')]
        [InlineData('f', '7')]
        [InlineData('c', '6')]
        [InlineData('d', '6')]
        [InlineData('e', '6')]
        [InlineData('b', '5')]
        [InlineData('c', '5')]
        [InlineData('e', '5')]
        [InlineData('f', '5')]
        [InlineData('c', '4')]
        [InlineData('d', '4')]
        [InlineData('e', '4')]
        [InlineData('b', '3')]
        [InlineData('d', '3')]
        [InlineData('f', '3')]
        public void IsValidDadaUmaPosicaoInvalidaDeveRetornarFalse(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData('c', '7')]
        [InlineData('e', '7')]
        [InlineData('b', '6')]
        [InlineData('f', '6')]
        [InlineData('b', '4')]
        [InlineData('f', '4')]
        [InlineData('c', '3')]
        [InlineData('e', '3')]
        public void IsValidDadaUmaPosicaoValidaDeveRetornarTrue(char file, char rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }
    }
}
