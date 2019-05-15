namespace Chess.Client.Scenarios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Client.Infra.UI;
    using Chess.Lib.Extensions;
    using Chess.Models;

    using static System.Threading.Thread;

    using static Chess.Client.Infra.UI.Color;
    using static Chess.Client.Infra.UI.DividerPosition;
    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Symbols;
    using static Chess.Client.Infra.UI.Writer;

    internal static class MatchScenario
    {
        private static readonly IReadOnlyDictionary<DividerPosition, Action> Dividers = new Dictionary<DividerPosition, Action>
        {
            { Top, () => WriteDivider(Board.Upper.Left, Board.Upper.Right, Board.Upper.Center) },
            { Middle, () => WriteDivider(Board.Middle.Left, Board.Middle.Right, Board.Middle.Center) },
            { Bottom, () => WriteDivider(Board.Bottom.Left, Board.Bottom.Right, Board.Bottom.Center) },
        };

        internal static void MovePiece(Action<string, string> done)
        {
            ClearOption();

            var (file, rank) = RequestOption("   NEXT MOVE -> piece ", left: 22);
            var piecePosition = new string(new[] { file, rank });

            var (newFile, newRank) = RequestOption(" move for ", left: 34);
            var newPosition = new string(new[] { newFile, newRank });

            done(piecePosition, newPosition);

            ClearOption();

            void ClearOption()
            {
                SetCursor(top: 23);

                for (var i = 0; i < 40; i++)
                {
                    WriteValue(' ');
                }

                SetCursor(top: 23);
            }

            (char, char) RequestOption(string text, int left)
            {
                text.ForEach(letter =>
                {
                    Sleep(60);
                    WriteValue(letter);
                });

                var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
                var ranks = new[] { '1', '2', '3', '4', '5', '6', '7', '8' };

                var readFile = ReadChar(() => SetCursor(top: 23, left: left), files.Contains, "insert between a and h");
                var readRank = ReadChar(() => SetCursor(top: 23, left: left + 1), ranks.Contains, "insert between 8 and 1");

                return (readFile, readRank);
            }
        }

        internal static void Draw(this Chessboard chessboard)
        {
            ClearScreen();

            chessboard.DrawHeaderOrFooter();
            Dividers[Top]();

            var whiteColor = true;

            for (var rank = chessboard.Ranks.Count - 1; rank > -1; rank--)
            {
                chessboard.DrawRank(rank, whiteColor);

                Dividers[rank == 0 ? Bottom : Middle]();

                whiteColor = !whiteColor;
            }

            chessboard.DrawHeaderOrFooter();
        }

        private static void DrawRank(this Chessboard chessboard, int rank, bool whiteColor)
        {
            WriteValue("   {0} ", rank + 1);

            for (var file = 0; file < chessboard.Files.Count; file++)
            {
                ApplyColor(whiteColor);

                chessboard.DrawFile(file, rank);

                whiteColor = !whiteColor;

                ApplyColor();
            }

            WritePipe();
            WriteValue(" {0} ", rank + 1);
            WriteNewLine();
        }

        private static void DrawFile(this Chessboard chessboard, int file, int rank)
        {
            WritePipe();

            var piece = chessboard[file, rank];
            if (piece == null)
            {
                WriteValue("   ");
            }
            else
            {
                WriteValue(" {0} ", piece);
            }
        }

        private static void DrawHeaderOrFooter(this Chessboard chessboard)
        {
            WriteValue("     ");

            chessboard
                .Files
                .ForEach(file => WriteValue("  {0} ", file));

            WriteNewLine();
        }

        private static void WriteDivider(char leftCorner, char rightCorner, char separator)
        {
            const int space = 3;
            const int block = 8;

            WriteValue("     {0}", leftCorner);

            for (var i = 0; i < block; i++)
            {
                for (var j = 0; j < space; j++)
                {
                    WriteValue(Board.Dash);
                }

                if (!i.Equals(7))
                {
                    WriteValue(separator);
                }
            }

            WriteValue(rightCorner);

            WriteNewLine();
        }

        private static void WritePipe()
        {
            var backgroundColor = CurrentBackgroundColor;

            ApplyColor();

            WriteValue(Board.Pipe);

            if (backgroundColor == ConsoleColor.White)
            {
                ApplyColor(secondColor: true);
            }
        }
    }
}
