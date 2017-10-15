namespace Chess.Test.Validations
{
    using Chess.Entities;
    using Chess.Lib.Validations;
    using Chess.Lib.Validations.BishopValidations;

    using Moq;

    using Xunit;

    public class QueenValidatorTests
    {
        private readonly Mock<FileAndRankLimitValidate> bishopFileAndRankLimitValidateMock;
        private readonly Mock<Lib.Validations.RookValidations.FileAndRankLimitValidate> rookFileAndRankLimitValidateMock;

        public QueenValidatorTests()
        {
            this.bishopFileAndRankLimitValidateMock = new Mock<FileAndRankLimitValidate>();
            this.rookFileAndRankLimitValidateMock = new Mock<Lib.Validations.RookValidations.FileAndRankLimitValidate>();

            var validator = new QueenValidator(this.bishopFileAndRankLimitValidateMock.Object, this.rookFileAndRankLimitValidateMock.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Fact]
        public void ValidateDeveChamarBishopFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            this.bishopFileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Fact]
        public void ValidateDeveChamarBishopFileAndRankLimitValidateIsValidUmaVez()
        {
            this.bishopFileAndRankLimitValidateMock.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void ValidateDeveChamarRookFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            this.rookFileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }
    }
}