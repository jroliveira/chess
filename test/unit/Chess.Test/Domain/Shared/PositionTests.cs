namespace Chess.Test.Domain.Shared
{
    using Chess.Domain.Shared;

    using FluentAssertions;

    using Xunit;

    using static Chess.Domain.Shared.Direction;

    public class PositionTests
    {
        private readonly Position position;

        public PositionTests() => this.position = "d5";

        [Fact]
        public void ImplicitOperatorString_GivenPosition_ShouldReturn()
        {
            string actual = this.position;

            actual.Should().Be("d5");
        }

        [Fact]
        public void ToString_GivenPosition_ShouldReturn()
        {
            var actual = this.position.ToString();

            actual.Should().Be("d5");
        }

        [Theory]
        [InlineData("d6", false)]
        [InlineData("d5", true)]
        [InlineData("a5", false)]
        public void Equals_GivenOtherPosition_ShouldReturn(string other, bool expected)
        {
            var actual = this.position.Equals(other);

            actual.Should().Be(expected);
        }

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │   │   │   │   │   │   │   │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │   │   │   │ ● │   │   │   │   │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │   │ ● │ ↑ │   │   │   │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │ ● │ ← │ Q │ → │ → │ ● │   │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │   │ ↙ │ ↓ │ ↘ │   │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │   │ ↙ │   │ ↓ │   │ ● │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │ ● │   │   │ ↓ │   │   │   │   │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │ ● │   │   │   │   │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Theory]
        [InlineData("c6", 1, 1)]
        [InlineData("d7", 0, 2)]
        [InlineData("g5", 3, 0)]
        [InlineData("f3", 2, 2)]
        [InlineData("d1", 0, 4)]
        [InlineData("a2", 3, 3)]
        [InlineData("b5", 2, 0)]
        [InlineData("d5", 0, 0)]
        public void GetDistanceTo_GivenNewPosition_ShouldReturn(string newPosition, byte filesToMove, byte ranksToMove)
        {
            var expected = (filesToMove, ranksToMove);

            var actual = this.position.GetDistanceTo(newPosition);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("c6", Top, Left)]
        [InlineData("d7", Top, None)]
        [InlineData("g5", None, Right)]
        [InlineData("f3", Bottom, Right)]
        [InlineData("d1", Bottom, None)]
        [InlineData("a2", Bottom, Left)]
        [InlineData("b5", None, Left)]
        [InlineData("d5", None, None)]
        public void GetDirectionTo_GivenNewPosition_ShouldReturn(string newPosition, Direction horizontal, Direction vertical)
        {
            var expected = (horizontal, vertical);

            var actual = this.position.GetDirectionTo(newPosition);

            actual.Should().Be(expected);
        }
    }
}
