namespace Chess.Client.Scenarios.Match
{
    using Chess.Client.Infra.Extensions;
    using Chess.Lib.Extensions;
    using Chess.Models;

    using static System.Environment;

    using static Chess.Client.Infra.UI.Terminal;

    internal static class ChessboardExtension
    {
        internal static void Draw(this Chessboard @this)
        {
            @this.DrawHeaderOrFooter();
            var whiteColor = true;

            for (var rank = @this.Ranks.Count - 1; rank > -1; rank--)
            {
                WriteBlocks(whiteColor);
                @this.DrawRank(rank, whiteColor);
                WriteBlocks(whiteColor);

                whiteColor = !whiteColor;
            }

            @this.DrawHeaderOrFooter();
        }

        private static void DrawHeaderOrFooter(this Chessboard @this)
        {
            ResetScreenColor();

            WriteValue(' '.Repeat(6));

            @this
                .Files
                .ForEach(file => WriteValue($"  {file}  "));

            WriteValue($"   {NewLine}");
        }

        private static void DrawRank(this Chessboard @this, int rank, bool isWhiteColor)
        {
            ResetScreenColor();
            WriteValue($"    {rank + 1} ");

            for (var file = 0; file < @this.Files.Count; file++)
            {
                ApplyBoardColor(isWhiteColor);

                @this.DrawFile(file, rank);

                isWhiteColor = !isWhiteColor;

                ApplyBoardColor();
            }

            ResetScreenColor();
            WriteValue($" {rank + 1} {NewLine}");
        }

        private static void DrawFile(this Chessboard @this, int file, int rank)
        {
            var piece = @this[file, rank];

            if (piece == null)
            {
                WriteValue(' '.Repeat(5));
            }
            else
            {
                WriteValue($"  {piece}  ");
            }
        }

        private static void WriteBlocks(bool isWhiteColor)
        {
            const int blocks = 8;

            ResetScreenColor();
            WriteValue(' '.Repeat(6));

            for (var block = 0; block < blocks; block++)
            {
                ApplyBoardColor(isWhiteColor);

                WriteValue(' '.Repeat(5));

                isWhiteColor = !isWhiteColor;

                ApplyBoardColor();
            }

            ResetScreenColor();
            WriteValue($"   {NewLine}");
        }
    }
}
