namespace Chess.Orleans.ConsoleApp.Scenarios.Match
{
    using Chess;
    using Chess.Infra.Monad;
    using Chess.Orleans.Contract;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;
    using static Chess.Orleans.ConsoleApp.Scenarios.Match.MatchScenario;

    internal class UserGrain : IUserGrain
    {
        private readonly Option<string> userNameOption;
        private readonly MatchScenario matchScenario;

        public UserGrain(Option<string> userNameOption)
        {
            this.userNameOption = userNameOption;
            this.matchScenario = new MatchScenario();
        }

        public void GameChanged(Try<Match> tryMatch) => tryMatch.Match(
            exception => Task(ShowError(exception, confirm: true)),
            async match =>
            {
                await this.matchScenario.ShowScenario(match);
                return Unit();
            });

        public void YourMove(Try<Match> tryMatch, IMatchGrain matchGrain) => tryMatch.Match(
            exception => ShowError(exception, confirm: true),
            match => MovePiece(
                match,
                move => matchGrain
                    .MovePiece(move.PiecePosition, move.NewPosition, this.userNameOption)
                    .GetAwaiter()
                    .GetResult()
                    .Match(
                        exception =>
                        {
                            ShowError(exception, confirm: true);
                            this.YourMove(match, matchGrain);
                            return Unit();
                        },
                        newMatch =>
                        {
                            this.matchScenario.ShowScenario(newMatch);
                            return Unit();
                        })));
    }
}
