using Chess.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Test.Exceptions
{
    [TestFixture]
    public class ChessExceptionTests
    {
        private ChessException _exception;

        [Test]
        public void Message_DadaMensagemSemParametro_DeveRetornarMensagem()
        {
            _exception = new ChessException("Não é possível mover a peça.");

            _exception.Message.Should().Be("Não é possível mover a peça.");
        }

        [Test]
        public void Message_DadaMensagemComParametro_DeveRetornarMensagem()
        {
            _exception = new ChessException("Não é possível mover a peça '{0}'.", "a1");

            _exception.Message.Should().Be("Não é possível mover a peça 'a1'.");
        }
    }
}
