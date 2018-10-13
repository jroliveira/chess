namespace Chess.Client
{
    using Chess.Client.UI.Extensions;
    using Chess.Interfaces;
    using Chess.Lib.Monad;
    using Chess.Models;

    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Client.UI.Scenarios.MatchScenario;

    internal class Player : IPlayer
    {
        private Option<string> playerName;

        public void GameChanged(Try<Chessboard> chessboard) => chessboard.Match(
            exception => WriteError(exception.Message),
            board => board.Draw());

        public void YourMove(IMatch match) => MovePiece((piecePosition, newPosition) =>
        {
            var chessboard = match
                .MovePiece(piecePosition, newPosition, this.playerName)
                .GetAwaiter()
                .GetResult();

            chessboard.Match(
                exception =>
                {
                    WriteError(exception.Message);

                    this.YourMove(match);
                },
                _ => { });
        });

        public void SetName(Option<string> name) => this.playerName = name;
    }
}
