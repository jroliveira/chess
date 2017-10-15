namespace Chess.Test.Extensions
{
    using Chess.Lib.Extensions;

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
        public void IsLastDadoCharCDeveRetornarTrue()
        {
            var actual = this.collection.IsLast('c');

            actual.Should().BeTrue();
        }

        [Fact]
        public void IsLastDadoCharBDeveRetornarFalse()
        {
            var actual = this.collection.IsLast('b');

            actual.Should().BeFalse();
        }
    }
}
