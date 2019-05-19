namespace Chess.Client.Scenarios.Match
{
    using Chess.Interfaces;
    using Chess.Lib.Monad;
    using Chess.Models;

    using static Chess.Client.Infra.UI.Terminal;
    using static Chess.Client.Scenarios.Match.MatchScenario;

    internal class Player : IPlayer
    {
        private readonly Option<string> name;

        public Player(Option<string> name) => this.name = name;

        public void GameChanged(Try<Chessboard> chessboard) => chessboard.Match(
            exception => WriteError(exception.Message),
            board => ShowMatch(board));

        public void YourMove(IMatch match) => MovePiece((piecePosition, newPosition) =>
        {
            var chessboard = match
                .MovePiece(piecePosition, newPosition, this.name)
                .GetAwaiter()
                .GetResult();

            chessboard.Match(
                exception =>
                {
                    WriteError(exception.Message);
                    this.YourMove(match);
                },
                board => ShowMatch(board));
        });
    }
}
