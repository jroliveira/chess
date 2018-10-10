namespace Chess.Test.Validations
{
    using Chess.Entities;
    using Chess.Lib.Validations;
    using Chess.Lib.Validations.BishopValidations;

    using Moq;

    using Xunit;

    public class BishopValidatorTests
    {
        private readonly Mock<FileAndRankLimitValidate> fileAndRankLimitValidateMock;

        public BishopValidatorTests()
        {
            this.fileAndRankLimitValidateMock = new Mock<FileAndRankLimitValidate>();

            var validator = new BishopValidator(this.fileAndRankLimitValidateMock.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Fact]
        public void ValidateDeveChamarFileAndRankLimitValidateSetNextValidateUmaVez() => this.fileAndRankLimitValidateMock
            .Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);

        [Fact]
        public void ValidateDeveChamarFileAndRankLimitValidateIsValidUmaVez() => this.fileAndRankLimitValidateMock
            .Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
    }
}
