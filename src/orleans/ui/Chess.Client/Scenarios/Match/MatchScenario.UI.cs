namespace Chess.Client.Scenarios.Match
{
    using System;
    using System.Linq;
    using Chess.Client.Infra.Extensions;
    using Chess.Models;

    using static System.ConsoleColor;
    using static System.Environment;

    using static Chess.Client.Infra.UI.Terminal;

    internal static partial class MatchScenario
    {
        private const string InputLabel = " piece ";
        private const int InputLength = 23;
        private static readonly int InputArea = InputLabel.Length + InputLength;

        internal static void DrawScenario(Chessboard chessboard)
        {
            ClearScreen();
            SetCursorPosition(top: 6);

            chessboard.Draw();
        }

        internal static void DrawScenario()
        {
            SetCursorPosition(top: 33);

            Write(' '.Repeat(InputArea));
            Write($"{InputLabel}{' '.Repeat(InputLength)}");
            Write(' '.Repeat(InputArea));

            (string Left, string Text, string Right) Write(string text)
            {
                var value = text.Center(ScreenWidth);

                WriteValue(value.Left);
                ApplyScreenColor(background: DarkGray);
                WriteValue(value.Text);
                ResetScreenColor();
                WriteValue(value.Right);

                return value;
            }
        }

        internal static (string PiecePosition, string NewPosition) GetNextMove()
        {
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
                        SetCursorPosition(top: 34, left: left);
                    },
                    files.Contains,
                    "Invalid file.");

                var readRank = ReadChar(
                    () =>
                    {
                        ApplyScreenColor(background: DarkGray, foreground: Black);
                        SetCursorPosition(top: 34, left: left + 1);
                    },
                    ranks.Contains,
                    "Invalid rank.");

                return (readFile, readRank);
            }
        }

        private static void PrintSuccessMessage() => WriteInfo("Waiting to play.");
    }
}
