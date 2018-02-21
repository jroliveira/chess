namespace Chess.Test
{
    using Chess.Entities;

    using FluentAssertions;

    using Xunit;

    public class PositionTests
    {
        private readonly Position position;

        public PositionTests() => this.position = new Position('a', 1);

        [Fact]
        public void ToStringDadaPosicaoA1DeveRetornarStringA1()
        {
            var actual = this.position.ToString();

            actual.Should().Be("a1");
        }

        [Theory]
        [InlineData('b', 1, false)]
        [InlineData('a', 2, false)]
        [InlineData('a', 1, true)]
        public void EqualsDadaPosicaoEPosicaoDeveRetornar(char file, uint rank, bool expected)
        {
            var other = new Position(file, rank);

            var actual = this.position.Equals(other);

            actual.Should().Be(expected);
        }
    }
}
