namespace Chess.Test.Exceptions
{
    using Chess.Exceptions;

    using FluentAssertions;

    using Xunit;

    public class ChessExceptionTests
    {
        private ChessException exception;

        [Fact]
        public void Message_DadaMensagemSemParametro_DeveRetornarMensagem()
        {
            this.exception = new ChessException("Não é possível mover a peça.");

            this.exception.Message.Should().Be("Não é possível mover a peça.");
        }

        [Fact]
        public void Message_DadaMensagemComParametro_DeveRetornarMensagem()
        {
            this.exception = new ChessException("Não é possível mover a peça '{0}'.", "a1");

            this.exception.Message.Should().Be("Não é possível mover a peça 'a1'.");
        }
    }
}
