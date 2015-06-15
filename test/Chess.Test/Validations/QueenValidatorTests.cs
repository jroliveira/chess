using Chess.Validations;
using Moq;
using NUnit.Framework;
using Bishop = Chess.Validations.BishopValidations;
using Rook = Chess.Validations.RookValidations;

namespace Chess.Test.Validations
{
    [TestFixture]
    public class QueenValidatorTests
    {
        private Mock<Bishop.FileAndRankLimitValidate> _bishopFileAndRankLimitValidateMock;
        private Mock<Rook.FileAndRankLimitValidate> _rookFileAndRankLimitValidateMock;

        [SetUp]
        public void Setup()
        {
            _bishopFileAndRankLimitValidateMock = new Mock<Bishop.FileAndRankLimitValidate>();
            _rookFileAndRankLimitValidateMock = new Mock<Rook.FileAndRankLimitValidate>();

            var validator = new QueenValidator(_bishopFileAndRankLimitValidateMock.Object, _rookFileAndRankLimitValidateMock.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Test]
        public void Validate_DeveChamarBishopFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            _bishopFileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarBishopFileAndRankLimitValidateIsValidUmaVez()
        {
            _bishopFileAndRankLimitValidateMock.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarRookFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            _rookFileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }
    }
}