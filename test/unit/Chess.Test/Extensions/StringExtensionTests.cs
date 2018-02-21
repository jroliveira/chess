namespace Chess.Test.Extensions
{
    using Chess.Entities;
    using Chess.Lib.Extensions;

    using FluentAssertions;

    using Xunit;

    public class StringExtensionTests
    {
        [Theory]
        [InlineData("a2", 'a', 2)]
        [InlineData("h8", 'h', 8)]
        [InlineData("c5", 'c', 5)]
        public void ToPositionDadaPosicaoDeveRetornarPosicao(string position, char file, uint rank)
        {
            var actual = position.ToPosition();
            var expected = new Position(file, rank);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
