using Chess.Test.Fakes.Validations;
using Chess.Validations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Validations
{
    [TestFixture]
    public class ValidateTests
    {
        private Mock<Validate> _validateMock;
        private Validate _validateFake;

        [SetUp]
        public void Setup()
        {
            _validateMock = new Mock<Validate>();
            _validateMock.Setup(m => m.IsValid(It.IsAny<Position>())).Returns(true);

            _validateFake = new FakeValidate(true);
            _validateFake.SetNextValidate(_validateMock.Object);
        }

        [Test]
        public void IsValid_DadaUmaPosicaoInvalidaSemNextValidate_DeveRetornarFalse()
        {
            _validateFake = new FakeValidate(false);

            var actual = _validateFake.IsValid(It.IsAny<Position>());

            actual.Should().BeFalse();
        }

        [Test]
        public void IsValid_DadaUmaPosicaoInvalidaComNextValidate_DeveChamarNextValidateIsValidUmaVez()
        {
            _validateFake = new FakeValidate(false);
            _validateFake.SetNextValidate(_validateMock.Object);

            _validateFake.IsValid(It.IsAny<Position>());

            _validateMock.Verify(m => m.IsValid(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void IsValid_DadaUmaPosicaoValida_DeveRetornarTrue()
        {
            var actual = _validateFake.IsValid(It.IsAny<Position>());

            actual.Should().BeTrue();
        }

        [Test]
        public void IsValid_DadaUmaPosicaoValidaComNextValidate_NaoDeveChamarNextValidateIsValid()
        {
            _validateFake.IsValid(It.IsAny<Position>());

            _validateMock.Verify(m => m.IsValid(It.IsAny<Position>()), Times.Never);
        }
    }
}
