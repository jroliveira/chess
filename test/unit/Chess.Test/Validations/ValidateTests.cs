namespace Chess.Test.Validations
{
    using Chess.Entities;
    using Chess.Lib.Validations;
    using Chess.Test.Fakes.Validations;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class ValidateTests
    {
        private readonly Mock<Validate> validateMock;
        private Validate validate;

        public ValidateTests()
        {
            this.validateMock = new Mock<Validate>();
            this.validateMock.Setup(m => m.IsValid(It.IsAny<Position>())).Returns(true);

            this.validate = new FakeValidate(true);
            this.validate.SetNextValidate(this.validateMock.Object);
        }

        [Fact]
        public void IsValidDadaUmaPosicaoInvalidaSemNextValidateDeveRetornarFalse()
        {
            this.validate = new FakeValidate(false);

            var actual = this.validate.IsValid(It.IsAny<Position>());

            actual.Should().BeFalse();
        }

        [Fact]
        public void IsValidDadaUmaPosicaoInvalidaComNextValidateDeveChamarNextValidateIsValidUmaVez()
        {
            this.validate = new FakeValidate(false);
            this.validate.SetNextValidate(this.validateMock.Object);

            this.validate.IsValid(It.IsAny<Position>());

            this.validateMock.Verify(m => m.IsValid(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void IsValidDadaUmaPosicaoValidaDeveRetornarTrue()
        {
            var actual = this.validate.IsValid(It.IsAny<Position>());

            actual.Should().BeTrue();
        }

        [Fact]
        public void IsValidDadaUmaPosicaoValidaComNextValidateNaoDeveChamarNextValidateIsValid()
        {
            this.validate.IsValid(It.IsAny<Position>());

            this.validateMock.Verify(m => m.IsValid(It.IsAny<Position>()), Times.Never);
        }
    }
}
