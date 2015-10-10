namespace Chess.Test.Validations
{
    using Chess.Validations;
    using Chess.Validations.PawnValidations;

    using Moq;

    using Xunit;

    public class PawnValidatorTests
    {
        private readonly Mock<FileAndRankLimitValidate> fileAndRankLimitValidateMock;

        public PawnValidatorTests()
        {
            this.fileAndRankLimitValidateMock = new Mock<FileAndRankLimitValidate>();

            var validator = new PawnValidator(this.fileAndRankLimitValidateMock.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Fact]
        public void Validate_DeveChamarFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            this.fileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Fact]
        public void Validate_DeveChamarFileAndRankLimitValidateIsValidUmaVez()
        {
            this.fileAndRankLimitValidateMock.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }
    }
}