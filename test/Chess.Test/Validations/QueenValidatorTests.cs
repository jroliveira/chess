namespace Chess.Test.Validations
{
    using Chess.Validations;

    using Moq;

    using NUnit.Framework;

    using Bishop = Chess.Validations.BishopValidations;
    using Rook = Chess.Validations.RookValidations;

    [TestFixture]
    public class QueenValidatorTests
    {
        private Mock<Bishop.FileAndRankLimitValidate> bishopFileAndRankLimitValidateMock;
        private Mock<Rook.FileAndRankLimitValidate> rookFileAndRankLimitValidateMock;

        [SetUp]
        public void Setup()
        {
            this.bishopFileAndRankLimitValidateMock = new Mock<Bishop.FileAndRankLimitValidate>();
            this.rookFileAndRankLimitValidateMock = new Mock<Rook.FileAndRankLimitValidate>();

            var validator = new QueenValidator(this.bishopFileAndRankLimitValidateMock.Object, this.rookFileAndRankLimitValidateMock.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Test]
        public void Validate_DeveChamarBishopFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            this.bishopFileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarBishopFileAndRankLimitValidateIsValidUmaVez()
        {
            this.bishopFileAndRankLimitValidateMock.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarRookFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            this.rookFileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }
    }
}