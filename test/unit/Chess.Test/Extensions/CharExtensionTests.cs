namespace Chess.Test.Extensions
{
    using Chess.Extensions;

    using FluentAssertions;

    using Xunit;

    public class CharExtensionTests
    {
        private readonly char[] collection;

        public CharExtensionTests()
        {
            this.collection = new[] { 'a', 'b', 'c' };
        }

        [Fact]
        public void IsFirst_DadoCharA_DeveRetornarTrue()
        {
            var actual = this.collection.IsFirst('a');

            actual.Should().BeTrue();
        }

        [Fact]
        public void IsFirst_DadoCharB_DeveRetornarFalse()
        {
            var actual = this.collection.IsFirst('b');

            actual.Should().BeFalse();
        }

        [Fact]
        public void IsLast_DadoCharC_DeveRetornarTrue()
        {
            var actual = this.collection.IsLast('c');

            actual.Should().BeTrue();
        }

        [Fact]
        public void IsLast_DadoCharB_DeveRetornarFalse()
        {
            var actual = this.collection.IsLast('b');

            actual.Should().BeFalse();
        }
    }
}
