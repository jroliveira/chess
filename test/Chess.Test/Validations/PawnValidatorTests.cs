using Chess.Game;
using Chess.Game.Validations;
using Chess.Game.Validations.PawnValidations;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Validations
{
    [TestFixture]
    public class PawnValidatorTests
    {
        private Mock<FileAndRankLimitValidate> _fileAndRankLimitValidate;
        private Mock<HasPieceValidate> _hasPieceValidate;

        [SetUp]
        public void Setup()
        {
            _fileAndRankLimitValidate = new Mock<FileAndRankLimitValidate>();
            _hasPieceValidate = new Mock<HasPieceValidate>();

            var validator = new PawnValidator(_fileAndRankLimitValidate.Object, _hasPieceValidate.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Test]
        public void Validate_DeveChamarFileAndRankLimitValidateSetNextValidate_UmaVez()
        {
            _fileAndRankLimitValidate.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarHasPieceValidateSetNextValidate_UmaVez()
        {
            _hasPieceValidate.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarFileAndRankLimitValidateIsValid_UmaVez()
        {
            _fileAndRankLimitValidate.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }
    }
}