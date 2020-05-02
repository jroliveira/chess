namespace Chess.Orleans.ConsoleApp.Scenarios.Match
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess;
    using Chess.Infra.Monad;
    using Chess.Orleans.ConsoleApp.Scenarios.Shared;

    using static System.ConsoleColor;

    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    internal sealed class MatchScenario : ScenarioBase
    {
        internal static Unit MovePiece(Match match, Func<(string PiecePosition, string NewPosition), Unit> done)
        {
            ClearMessage();
            match.Draw();

            SetCursorPosition(top: 36);
            WriteTextBox(" Piece ");

            return done(GetNextMove());
        }

        protected override Task<GameData> DrawScenario(GameData data)
        {
            data.MatchOption.Draw();

            ShowInfo("Waiting to play", clean: false);

            return base.DrawScenario(data);
        }

        private static (string PiecePosition, string NewPosition) GetNextMove()
        {
            const int topPosition = 37;

            var (file, rank) = RequestOption(left: 19);
            var piecePosition = new string(new[] { file, rank });

            ApplyScreenColor(background: DarkGray, foreground: Black);
            WriteValue(" to ");

            var (newFile, newRank) = RequestOption(left: 25);
            var newPosition = new string(new[] { newFile, newRank });

            return (piecePosition, newPosition);

            (char, char) RequestOption(int left)
            {
                var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
                var ranks = new[] { '1', '2', '3', '4', '5', '6', '7', '8' };

                var readFile = ReadChar(
                    () =>
                    {
                        ApplyScreenColor(background: DarkGray, foreground: Black);
                        SetCursorPosition(top: topPosition, left: left);
                    },
                    files.Contains,
                    "Invalid file");

                var readRank = ReadChar(
                    () =>
                    {
                        ApplyScreenColor(background: DarkGray, foreground: Black);
                        SetCursorPosition(top: topPosition, left: left + 1);
                    },
                    ranks.Contains,
                    "Invalid rank");

                return (readFile, readRank);
            }
        }
    }
}
