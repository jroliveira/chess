namespace Chess.Test
{
    using FluentAssertions;

    using Xunit;

    public class PositionTests
    {
        private readonly Position position;

        public PositionTests()
        {
            this.position = new Position('a', '1');
        }

        [Fact]
        public void ToString_DadaPosicaoA1_DeveRetornarStringA1()
        {
            var actual = this.position.ToString();

            actual.Should().Be("a1");
        }

        [Theory]
        [InlineData('b', '1', false)]
        [InlineData('a', '2', false)]
        [InlineData('a', '1', true)]
        public void Equals_DadaPosicaoEPosicao_DeveRetornar(char file, char rank, bool expected)
        {
            var other = new Position(file, rank);

            var actual = this.position.Equals(other);

            actual.Should().Be(expected);
        }
    }
}
