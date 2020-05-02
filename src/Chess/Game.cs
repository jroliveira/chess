namespace Chess
{
    using System.Linq;

    using Chess.Domain.Chessboard;
    using Chess.Domain.User;
    using Chess.Infra.Monad;
    using Chess.Infra.Monad.Extensions;
    using Chess.Infra.Monad.Linq;

    using static System.String;

    using static Chess.Constants.ErrorMessages;
    using static Chess.Constants.ErrorMessages.Piece;
    using static Chess.Constants.ErrorMessages.User;
    using static Chess.Domain.Chessboard.MountChessboardCommand;
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
            Option<PieceColor> playingWithOption = default)
        {
            var userName = userNameOption.GetOrElse(Empty);
            if (userName == Empty)
            {
                return CannotBeNullOrEmpty("User name");
            }

            return this.users
                .AddUser(userName, playingWithOption)
                .Select(_ => CreateMatch(
                    this.chessboard,
                    this.users.Players.Select(item => item.ToUser()).ToList(),
                    this.users.Spectators.Select(item => item.ToUser()).ToList()));
        }

        public Try<Match> MovePiece(
            Option<string> piecePositionOption,
            Option<string> newPositionOption,
            Option<string> userNameOption)
        {
            var piecePosition = piecePositionOption.GetOrElse(Empty);
            if (piecePosition == Empty)
            {
                return CannotBeNullOrEmpty("Piece position");
            }

            var newPosition = newPositionOption.GetOrElse(Empty);
            if (newPosition == Empty)
            {
                return CannotBeNullOrEmpty("New position");
            }

            var userName = userNameOption.GetOrElse(Empty);
            if (userName == Empty)
            {
                return CannotBeNullOrEmpty("Player");
            }

            var user = this.users.GetUser(userName).GetOrElse(null);
            if (user == null || !(user is Player player))
            {
                return IsNotPlaying(userName);
            }

            var piece = this.chessboard.GetPiece(piecePosition).GetOrElse(null);
            if (piece == null)
            {
                return DoesNotExist(piecePosition);
            }

            if (!piece.BelongsTo(player))
            {
                return DoesNotBelongToYou(piece);
            }

            return this.chessboard
                .MovePiece(piece, newPosition)
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
