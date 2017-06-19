namespace Chess.Test.Validations
{
    using Chess.Validations;
    using Chess.Validations.KnightValidations;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class KnightValidatorTests
    {
        private Mock<FileAndRankLimitValidate> fileAndRankLimitValidateMock;

        [SetUp]
        public void Setup()
        {
            this.fileAndRankLimitValidateMock = new Mock<FileAndRankLimitValidate>();

            var validator = new KnightValidator(this.fileAndRankLimitValidateMock.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Test]
        public void Validate_DeveChamarFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            this.fileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarFileAndRankLimitValidateIsValidUmaVez()
        {
            this.fileAndRankLimitValidateMock.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }
    }
}