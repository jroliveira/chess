using Chess.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Test.Extensions
{
    [TestFixture]
    public class CharExtensionTests
    {
        private char[] _collection;

        [SetUp]
        public void SetUp()
        {
            _collection = new[] { 'a', 'b', 'c' };
        }

        [Test]
        public void IsFirst_DadoCharA_DeveRetornarTrue()
        {
            var actual = _collection.IsFirst('a');

            actual.Should().BeTrue();
        }

        [Test]
        public void IsFirst_DadoCharB_DeveRetornarFalse()
        {
            var actual = _collection.IsFirst('b');

            actual.Should().BeFalse();
        }

        [Test]
        public void IsLast_DadoCharC_DeveRetornarTrue()
        {
            var actual = _collection.IsLast('c');

            actual.Should().BeTrue();
        }

        [Test]
        public void IsLast_DadoCharB_DeveRetornarFalse()
        {
            var actual = _collection.IsLast('b');

            actual.Should().BeFalse();
        }
    }
}
