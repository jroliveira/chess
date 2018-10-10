namespace Chess.Test.Validations.RookValidations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Validations.RookValidations;

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

            var rookStub = new Mock<Rook>();
            rookStub.Setup(p => p.Position).Returns(new Position('d', 5));
            rookStub.Setup(p => p.Chessboard).Returns(chessboardStub.Object);

            this.validate = new FileAndRankLimitValidate(rookStub.Object);
        }

        [Theory]
        [InlineData('b', 6)]
        [InlineData('c', 6)]
        [InlineData('e', 6)]
        [InlineData('f', 6)]
        [InlineData('d', 5)]
        [InlineData('c', 4)]
        [InlineData('e', 4)]
        public void IsValidDadaUmaPosicaoInvalidaDeveRetornarFalse(char file, uint rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData('d', 7)]
        [InlineData('d', 6)]
        [InlineData('b', 5)]
        [InlineData('c', 5)]
        [InlineData('e', 5)]
        [InlineData('f', 5)]
        [InlineData('d', 4)]
        [InlineData('d', 3)]
        public void IsValidDadaUmaPosicaoValidaDeveRetornarTrue(char file, uint rank)
        {
            var newPosition = new Position(file, rank);

            var actual = this.validate.IsValid(newPosition);

            actual.Should().BeTrue();
        }
    }
}
