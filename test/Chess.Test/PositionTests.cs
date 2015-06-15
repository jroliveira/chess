using FluentAssertions;
using NUnit.Framework;

namespace Chess.Test
{
    [TestFixture]
    public class PositionTests
    {
        private Position _position;

        [SetUp]
        public void SetUp()
        {
            _position = new Position('a', '1');
        }

        [Test]
        public void ToString_DadaPosicaoA1_DeveRetornarStringA1()
        {
            var actual = _position.ToString();

            actual.Should().Be("a1");
        }

        [TestCase('b', '1', false)]
        [TestCase('a', '2', false)]
        [TestCase('a', '1', true)]
        public void Equals_DadaPosicaoEPosicao_DeveRetornar(char file, char rank, bool expected)
        {
            var other = new Position(file, rank);

            var actual = _position.Equals(other);

            actual.Should().Be(expected);
        }
    }
}
