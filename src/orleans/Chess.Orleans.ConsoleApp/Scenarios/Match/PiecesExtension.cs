namespace Chess.Orleans.ConsoleApp.Scenarios.Match
{
    using Chess;
    using Chess.Infra.Monad;
    using Chess.Orleans.ConsoleApp.Infra.Extensions;

    using static System.Convert;
    using static System.Environment;

    using static Chess.Constants.Chessboard;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    internal static class PiecesExtension
    {
        internal static Unit Draw(this Pieces @this)
        {
            DrawHeaderOrFooter();
            var whiteColor = true;

            for (var rank = Ranks.Count - 1; rank > -1; rank--)
            {
                WriteBlocks(whiteColor);
                @this.DrawRank(ToByte(rank), whiteColor);
                WriteBlocks(whiteColor);

                whiteColor = !whiteColor;
            }

            return DrawHeaderOrFooter();
        }

        private static Unit DrawHeaderOrFooter()
        {
            ResetScreenColor();

            WriteValue(' '.Repeat(6));

            foreach (var file in Files)
            {
                WriteValue($"  {file}  ");
            }

            return WriteValue($"   {NewLine}");
        }

        private static void DrawRank(this Pieces @this, byte rank, bool isWhiteColor)
        {
            ResetScreenColor();
            WriteValue($"    {rank + 1} ");

            for (byte file = 0; file < Files.Count; file++)
            {
                ApplyBoardColor(isWhiteColor);

                @this.DrawFile(file, rank);

                isWhiteColor = !isWhiteColor;

                ApplyBoardColor();
            }

            ResetScreenColor();
            WriteValue($" {rank + 1} {NewLine}");
        }

        private static void DrawFile(this Pieces @this, byte file, byte rank)
        {
            var piece = @this[file, rank];

            WriteValue(piece == null
                ? ' '.Repeat(5)
                : $"  {piece}  ");
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
