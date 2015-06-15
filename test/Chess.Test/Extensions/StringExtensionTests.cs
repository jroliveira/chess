using Chess.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Test.Extensions
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase("a2", 'a', '2')]
        [TestCase("h8", 'h', '8')]
        [TestCase("c5", 'c', '5')]
        public void ToPosition_DadaPosicao_DeveRetornarPosicao(string position, char file, char rank)
        {
            var actual = position.ToPosition();
            var expected = new Position(file, rank);

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
