namespace Chess.Test.Validations
{
    using Chess.Validations;

    using Moq;

    using Xunit;

    using Bishop = Chess.Validations.BishopValidations;
    using Rook = Chess.Validations.RookValidations;

    public class QueenValidatorTests
    {
        private readonly Mock<Bishop.FileAndRankLimitValidate> bishopFileAndRankLimitValidateMock;
        private readonly Mock<Rook.FileAndRankLimitValidate> rookFileAndRankLimitValidateMock;

        public QueenValidatorTests()
        {
            this.bishopFileAndRankLimitValidateMock = new Mock<Bishop.FileAndRankLimitValidate>();
            this.rookFileAndRankLimitValidateMock = new Mock<Rook.FileAndRankLimitValidate>();

            var validator = new QueenValidator(this.bishopFileAndRankLimitValidateMock.Object, this.rookFileAndRankLimitValidateMock.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Fact]
        public void Validate_DeveChamarBishopFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            this.bishopFileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Fact]
        public void Validate_DeveChamarBishopFileAndRankLimitValidateIsValidUmaVez()
        {
            this.bishopFileAndRankLimitValidateMock.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void Validate_DeveChamarRookFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            this.rookFileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }
    }
}