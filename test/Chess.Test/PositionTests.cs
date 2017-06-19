namespace Chess.Test
{
    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class PositionTests
    {
        private Position position;

        [SetUp]
        public void SetUp()
        {
            this.position = new Position('a', '1');
        }

        [Test]
        public void ToString_DadaPosicaoA1_DeveRetornarStringA1()
        {
            var actual = this.position.ToString();

            actual.Should().Be("a1");
        }

        [TestCase('b', '1', false)]
        [TestCase('a', '2', false)]
        [TestCase('a', '1', true)]
        public void Equals_DadaPosicaoEPosicao_DeveRetornar(char file, char rank, bool expected)
        {
            var other = new Position(file, rank);

            var actual = this.position.Equals(other);

            actual.Should().Be(expected);
        }
    }
}
