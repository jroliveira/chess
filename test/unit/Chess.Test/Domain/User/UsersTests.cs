namespace Chess.Test.Domain.User
{
    using System.Collections.Generic;

    using Chess;
    using Chess.Domain.User;
    using Chess.Infra.Monad.Extensions;

    using FluentAssertions;

    using Xunit;

    using static Chess.Domain.User.Player;
    using static Chess.PieceColor;

    public class UsersTests
    {
        [Fact]
        public void AddUser_GivenExistingUserName_ShouldReturnException()
        {
            var expected = new ChessException("User name 'test' is already in use");
            var users = new Users(new List<UserBase> { CreatePlayer("test", BlackPiece) });

            var actual = users
                .AddUser("test", WhitePiece)
                .Match(_ => _, _ => default);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void AddUser_GivenExistingPieceColor_ShouldReturnException()
        {
            var expected = new ChessException("Piece color 'WhitePiece' is already in use");
            var users = new Users(new List<UserBase> { CreatePlayer("test", WhitePiece) });

            var actual = users
                .AddUser("user", WhitePiece)
                .Match(_ => _, _ => default);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void AddUser_GivenUser_ShouldReturnUser()
        {
            var expected = CreatePlayer("test", WhitePiece);
            var users = new Users();

            var actual = users
                .AddUser("test", WhitePiece)
                .Match(_ => default, _ => _);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void GetUser_GivenExistingUser_ShouldReturnUser()
        {
            var expected = CreatePlayer("test", WhitePiece);
            var users = new Users(new List<UserBase> { CreatePlayer("test", WhitePiece) });

            var actual = users.GetUser("test").GetOrElse(default);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void GetUser_GivenNonExistingUser_ShouldReturnUser()
        {
            const bool expected = false;
            var users = new Users();

            var actual = users.GetUser("test").IsDefined;

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
