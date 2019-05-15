namespace Chess.Client.Infra.UI
{
    using System;
    using System.Collections.Generic;

    using static System.Math;

    using static Chess.Client.Infra.UI.DividerPosition;
    using static Chess.Client.Infra.UI.Writer;

    internal static class WriterBox
    {
        private static readonly IReadOnlyDictionary<DividerPosition, Action<int>> Dividers = new Dictionary<DividerPosition, Action<int>>
        {
            { Top,    length => Divider('╔', '╗', length) },
            { Bottom, length => Divider('╚', '╝', length) },
        };

        internal static void WriteBox(int textLength, Action writeText)
        {
            var left = Abs(Console.CursorLeft - 1);
            var top = Console.CursorTop;

            SetCursor(top: 0);

            const int lengthMargin = 4;

            Dividers[Top](textLength + lengthMargin);
            WriteValue("   ║  ");

            writeText();

            WriteValue("  ║");

            WriteNewLine();
            Dividers[Bottom](textLength + lengthMargin);

            SetCursor(top: top, left: left);
        }

        private static void Divider(char leftCorner, char rightCorner, int length)
        {
            WriteValue("   {0}", leftCorner);

            for (var j = 0; j < length; j++)
            {
                WriteValue('═');
            }

            WriteValue(rightCorner);

            WriteNewLine();
        }
    }
}
