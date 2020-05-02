namespace Chess.Test.Domain.User
{
    using Chess;

    using FluentAssertions;

    using Xunit;

    using static Chess.Domain.User.Player;
    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.PieceColor;
    using static Chess.User;

    public class PlayerTests
    {
        [Theory]
        [InlineData(WhitePiece, WhitePiece)]
        [InlineData(BlackPiece, BlackPiece)]
        public void PlayingWith_GivenPlayingWith_ShouldReturn(PieceColor playingWith, PieceColor expected)
        {
            var actual = CreatePlayer("test", playingWith).PlayingWith;

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("sample", "sample")]
        public void ImplicitOperatorString_GivenPlayer_ShouldReturn(string name, string expected)
        {
            string actual = CreatePlayer(name, WhitePiece);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(WhitePiece, WhitePiece)]
        [InlineData(BlackPiece, BlackPiece)]
        public void ImplicitOperatorBoolean_GivenPlayer_ShouldReturn(PieceColor playingWith, PieceColor expected)
        {
            PieceColor actual = CreatePlayer("test", playingWith);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("sample", "sample")]
        public void ToString_GivenPlayer_ShouldReturn(string name, string expected)
        {
            var actual = CreatePlayer(name, WhitePiece).ToString();

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("test", WhitePiece, true)]
        [InlineData("test", BlackPiece, false)]
        [InlineData("user", WhitePiece, false)]
        public void Equals_GivenOtherPlayer_ShouldReturn(string name, PieceColor playingWith, bool expected)
        {
            var user = CreatePlayer("test", WhitePiece);

            var actual = user.Equals(CreatePlayer(name, playingWith));

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("test", true)]
        [InlineData("user", false)]
        public void Equals_GivenUserName_ShouldReturn(string name, bool expected)
        {
            var user = CreatePlayer("test", WhitePiece);

            var actual = user.Equals(CreatePlayer(name, WhitePiece));

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(WhitePiece, true)]
        [InlineData(BlackPiece, false)]
        public void Equals_GivenPlayerPlayingWith_ShouldReturn(PieceColor playingWith, bool expected)
        {
            var user = CreatePlayer("test", WhitePiece);

            var actual = user.Equals(CreatePlayer("test", playingWith));

            actual.Should().Be(expected);
        }

        [Fact]
        public void ToUser_GivenPlayer_ShouldReturn()
        {
            var expected = CreateUser("test", UserType.Player, Some(WhitePiece));

            var actual = CreatePlayer("test", WhitePiece).ToUser();

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
