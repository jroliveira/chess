namespace Chess.Test.Exceptions
{
    using Chess.Lib.Exceptions;

    using FluentAssertions;

    using Xunit;

    public class ChessExceptionTests
    {
        private ChessException exception;

        [Fact]
        public void MessageDadaMensagemSemParametroDeveRetornarMensagem()
        {
            this.exception = new ChessException("Cannot move the piece.");

            this.exception.Message.Should().Be("Cannot move the piece.");
        }

        [Fact]
        public void MessageDadaMensagemComParametroDeveRetornarMensagem()
        {
            this.exception = new ChessException("Cannot move the piece '{0}'.", "a1");

            this.exception.Message.Should().Be("Cannot move the piece 'a1'.");
        }
    }
}
