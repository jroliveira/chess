namespace Chess.Test.Extensions
{
    using Chess.Extensions;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class CharExtensionTests
    {
        private char[] collection;

        [SetUp]
        public void SetUp()
        {
            this.collection = new[] { 'a', 'b', 'c' };
        }

        [Test]
        public void IsFirst_DadoCharA_DeveRetornarTrue()
        {
            var actual = this.collection.IsFirst('a');

            actual.Should().BeTrue();
        }

        [Test]
        public void IsFirst_DadoCharB_DeveRetornarFalse()
        {
            var actual = this.collection.IsFirst('b');

            actual.Should().BeFalse();
        }

        [Test]
        public void IsLast_DadoCharC_DeveRetornarTrue()
        {
            var actual = this.collection.IsLast('c');

            actual.Should().BeTrue();
        }

        [Test]
        public void IsLast_DadoCharB_DeveRetornarFalse()
        {
            var actual = this.collection.IsLast('b');

            actual.Should().BeFalse();
        }
    }
}
