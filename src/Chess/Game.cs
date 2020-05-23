namespace Chess
{
    using System.Linq;

    using Chess.Domain.Chessboard;
    using Chess.Domain.Shared;
    using Chess.Domain.User;
    using Chess.Infra.Monad;

    using static Chess.Constants.ErrorMessages;
    using static Chess.Constants.ErrorMessages.UserError;
    using static Chess.Domain.Chessboard.MountChessboardCommand;
    using static Chess.Domain.Shared.Position;
    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Match;

    public sealed class Game : IGame
    {
        private readonly Users users;
        private Chessboard chessboard;

        public Game()
        {
            this.users = new Users();
            this.chessboard = MountChessboard();
        }

        public Try<Match> JoinUser(
            Option<string> userNameOption,
            Option<PieceColor> playingWithOption = default) => this.users
                .AddUser(userNameOption, playingWithOption)
                .Select(_ => CreateMatch(
                    this.chessboard,
                    this.users.Players.Select(item => item.ToUser()).ToList(),
                    this.users.Spectators.Select(item => item.ToUser()).ToList()));

        public Try<Match> MovePiece(
            Option<string> piecePositionOption,
            Option<string> newPositionOption,
            Option<string> userNameOption)
        {
            var tryPlayer = this.users
                .GetUser(userNameOption)
                .Select(user => user is Player player
                    ? Success(player)
                    : IsNotPlaying(user));

            return this.chessboard
                .MovePiece(
                    tryPlayer,
                    piecePositionOption.Fold(Failure<Position>(CannotBeNullOrEmpty("Piece position")))(CreatePosition),
                    newPositionOption.Fold(Failure<Position>(CannotBeNullOrEmpty("Piece position")))(CreatePosition))
                .Select(newChessboard =>
                {
                    this.chessboard = newChessboard;

                    return CreateMatch(
                        this.chessboard,
                        this.users.Players.Select(item => item.ToUser()).ToList(),
                        this.users.Spectators.Select(item => item.ToUser()).ToList());
                });
        }
    }
}
