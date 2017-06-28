namespace Chess.Test.Extensions
{
    using Chess.Extensions;

    using FluentAssertions;

    using Xunit;

    public class StringExtensionTests
    {
        [Theory]
        [InlineData("a2", 'a', '2')]
        [InlineData("h8", 'h', '8')]
        [InlineData("c5", 'c', '5')]
        public void ToPosition_DadaPosicao_DeveRetornarPosicao(string position, char file, char rank)
        {
            var actual = position.ToPosition();
            var expected = new Position(file, rank);

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
