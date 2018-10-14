namespace Chess.Client.UI.Extensions
{
    using Chess.Lib.Extensions;
    using Chess.Models;

    using static Chess.Client.Infra.UI.Color;
    using static Chess.Client.Infra.UI.DividerPosition;
    using static Chess.Client.Infra.UI.Writer;

    internal static class ChessboardExtension
    {
        internal static void Draw(this Chessboard chessboard)
        {
            ClearScreen();
            SetCursor();

            chessboard.DrawHeaderOrFooter();
            WriteDivider(Top);

            var whiteColor = true;

            for (var rank = chessboard.Ranks.Count - 1; rank > -1; rank--)
            {
                chessboard.DrawRank(rank, whiteColor);

                WriteDivider(rank == 0 ? Bottom : Middle);

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
    }
}
