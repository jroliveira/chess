namespace Chess.Test.Validations
{
    using Chess.Test.Fakes.Validations;
    using Chess.Validations;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ValidateTests
    {
        private Mock<Validate> validateMock;
        private Validate validate;

        [SetUp]
        public void Setup()
        {
            this.validateMock = new Mock<Validate>();
            this.validateMock.Setup(m => m.IsValid(It.IsAny<Position>())).Returns(true);

            this.validate = new FakeValidate(true);
            this.validate.SetNextValidate(this.validateMock.Object);
        }

        [Test]
        public void IsValid_DadaUmaPosicaoInvalidaSemNextValidate_DeveRetornarFalse()
        {
            this.validate = new FakeValidate(false);

            var actual = this.validate.IsValid(It.IsAny<Position>());

            actual.Should().BeFalse();
        }

        [Test]
        public void IsValid_DadaUmaPosicaoInvalidaComNextValidate_DeveChamarNextValidateIsValidUmaVez()
        {
            this.validate = new FakeValidate(false);
            this.validate.SetNextValidate(this.validateMock.Object);

            this.validate.IsValid(It.IsAny<Position>());

            this.validateMock.Verify(m => m.IsValid(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void IsValid_DadaUmaPosicaoValida_DeveRetornarTrue()
        {
            var actual = this.validate.IsValid(It.IsAny<Position>());

            actual.Should().BeTrue();
        }

        [Test]
        public void IsValid_DadaUmaPosicaoValidaComNextValidate_NaoDeveChamarNextValidateIsValid()
        {
            this.validate.IsValid(It.IsAny<Position>());

            this.validateMock.Verify(m => m.IsValid(It.IsAny<Position>()), Times.Never);
        }
    }
}
