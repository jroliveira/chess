using Chess.Pieces;
using Chess.Validations.PawnValidations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Validations.PawnValidations
{
    [TestFixture]
    public class FileAndRankLimitValidateTests
    {
        private FileAndRankLimitValidate _validate;

        [SetUp]
        public void Setup()
        {
            var pawn = new Pawn("b7".ToPosition(), It.IsAny<Chessboard>());
            _validate = new FileAndRankLimitValidate(pawn);
        }

        [TestCase("b7")]
        [TestCase("b4")]
        [TestCase("a7")]
        [TestCase("a5")]
        [TestCase("c7")]
        [TestCase("c5")]
        public void IsValid_DadoUmaPosicaoInvalida_DeveRetornarFalse(string newPosition)
        {
            var isValid = _validate.IsValid(newPosition.ToPosition());

            isValid.Should().BeFalse();
        }

        [TestCase("b8")]
        [TestCase("b6")]
        [TestCase("b5")]
        [TestCase("a8")]
        [TestCase("a6")]
        [TestCase("c8")]
        [TestCase("c6")]
        public void IsValid_DadoUmaPosicaoValida_DeveRetornarTrue(string newPosition)
        {
            var isValid = _validate.IsValid(newPosition.ToPosition());

            isValid.Should().BeTrue();
        }
    }
}
