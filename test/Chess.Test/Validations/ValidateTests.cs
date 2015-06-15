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
        private Validate _fakeValidate;

        [SetUp]
        public void Setup()
        {
            _validateMock = new Mock<Validate>();
            _validateMock.Setup(m => m.IsValid(It.IsAny<Position>())).Returns(true);

            _fakeValidate = new FakeValidate(true);
            _fakeValidate.SetNextValidate(_validateMock.Object);
        }

        [Test]
        public void IsValid_DadaUmaPosicaoInvalidaSemNextValidate_DeveRetornarFalse()
        {
            _fakeValidate = new FakeValidate(false);

            var actual = _fakeValidate.IsValid(It.IsAny<Position>());

            actual.Should().BeFalse();
        }

        [Test]
        public void IsValid_DadaUmaPosicaoInvalidaComNextValidate_DeveChamarNextValidateIsValidUmaVez()
        {
            _fakeValidate = new FakeValidate(false);
            _fakeValidate.SetNextValidate(_validateMock.Object);

            _fakeValidate.IsValid(It.IsAny<Position>());

            _validateMock.Verify(m => m.IsValid(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void IsValid_DadaUmaPosicaoValida_DeveRetornarTrue()
        {
            var actual = _fakeValidate.IsValid(It.IsAny<Position>());

            actual.Should().BeTrue();
        }

        [Test]
        public void IsValid_DadaUmaPosicaoValidaComNextValidate_NaoDeveChamarNextValidateIsValid()
        {
            _fakeValidate.IsValid(It.IsAny<Position>());

            _validateMock.Verify(m => m.IsValid(It.IsAny<Position>()), Times.Never);
        }
    }
}
