namespace Chess.Client.Scenarios
{
    using System;
    using System.Linq;
    using Chess.Lib.Extensions;
    using Chess.Models;

    using static System.Environment;

    using static Chess.Client.Infra.UI.Color;
    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Symbols.Board;
    using static Chess.Client.Infra.UI.Writer;

    internal static class MatchScenario
    {
        internal static void MovePiece(Action<string, string> done)
        {
            SetCursor(top: 33, left: 0);
            WriteValue($"   {Pipe} move:~$                                      {Pipe}{NewLine}");

            var (file, rank) = RequestOption(left: 13);
            var piecePosition = new string(new[] { file, rank });

            WriteValue(" to ");

            var (newFile, newRank) = RequestOption(left: 19);
            var newPosition = new string(new[] { newFile, newRank });

            done(piecePosition, newPosition);

            SetCursor(top: 33, left: 0);
            WriteValue($"   {Pipe} waiting to play...                           {Pipe}{NewLine}");

            (char, char) RequestOption(int left)
            {
                var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
                var ranks = new[] { '1', '2', '3', '4', '5', '6', '7', '8' };

                var readFile = ReadChar(() => SetCursor(top: 33, left: left), files.Contains, "Invalid file.");
                var readRank = ReadChar(() => SetCursor(top: 33, left: left + 1), ranks.Contains, "Invalid rank.");

                return (readFile, readRank);
            }
        }

        internal static void Draw(this Chessboard chessboard)
        {
            ClearScreen();

            WriteValue($"   {Upper.Left}");
            WriteValue(Dash, times: 46);
            WriteValue($"{Upper.Right}{NewLine}");

            chessboard.DrawHeaderOrFooter();
            var whiteColor = true;

            for (var rank = chessboard.Ranks.Count - 1; rank > -1; rank--)
            {
                WriteDivider(whiteColor);
                chessboard.DrawRank(rank, whiteColor);
                WriteDivider(whiteColor);

                whiteColor = !whiteColor;
            }

            chessboard.DrawHeaderOrFooter();

            WriteValue($"   {Middle.Left}");
            WriteValue(Dash, times: 46);
            WriteValue($"{Middle.Right}{NewLine}");
            WriteValue($"   {Pipe} waiting to play...                           {Pipe}{NewLine}");
            WriteValue($"   {Bottom.Left}");
            WriteValue(Dash, times: 46);
            WriteValue($"{Bottom.Right}{NewLine}");
        }

        private static void DrawRank(this Chessboard chessboard, int rank, bool whiteColor)
        {
            ResetColor();
            WriteValue($"   {Pipe} {rank + 1} ");

            for (var file = 0; file < chessboard.Files.Count; file++)
            {
                ApplyColor(whiteColor);

                chessboard.DrawFile(file, rank);

                whiteColor = !whiteColor;

                ApplyColor();
            }

            ResetColor();
            WriteValue($" {rank + 1} {Pipe}{NewLine}");
        }

        private static void DrawFile(this Chessboard chessboard, int file, int rank)
        {
            var piece = chessboard[file, rank];
            if (piece == null)
            {
                WriteValue(' ', times: 5);
            }
            else
            {
                WriteValue($"  {piece}  ");
            }
        }

        private static void DrawHeaderOrFooter(this Chessboard chessboard)
        {
            ResetColor();

            WriteValue($"   {Pipe}   ");

            chessboard
                .Files
                .ForEach(file => WriteValue($"  {file}  "));

            WriteValue($"   {Pipe}{NewLine}");
        }

        private static void WriteDivider(bool whiteColor)
        {
            const int blocks = 8;

            ResetColor();
            WriteValue($"   {Pipe}   ");

            for (var block = 0; block < blocks; block++)
            {
                ApplyColor(whiteColor);

                WriteValue(' ', times: 5);

                whiteColor = !whiteColor;

                ApplyColor();
            }

            ResetColor();
            WriteValue($"   {Pipe}{NewLine}");
        }
    }
}
