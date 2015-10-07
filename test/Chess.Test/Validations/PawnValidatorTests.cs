using Chess.Validations;
using Chess.Validations.PawnValidations;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Validations
{
    [TestFixture]
    public class PawnValidatorTests
    {
        private Mock<FileAndRankLimitValidate> _fileAndRankLimitValidate;

        [SetUp]
        public void Setup()
        {
            _fileAndRankLimitValidate = new Mock<FileAndRankLimitValidate>();

            var validator = new PawnValidator(_fileAndRankLimitValidate.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Test]
        public void Validate_DeveChamarFileAndRankLimitValidateSetNextValidate_UmaVez()
        {
            _fileAndRankLimitValidate.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarFileAndRankLimitValidateIsValid_UmaVez()
        {
            _fileAndRankLimitValidate.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }
    }
}