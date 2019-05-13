namespace Chess.Client.Infra.UI
{
    using System;
    using System.Collections.Generic;

    using static System.Environment;
    using static System.Math;

    using static Chess.Client.Infra.UI.Writer;

    internal static class WriterBox
    {
        internal static void WriteBox(int textLength, Action writeText)
        {
            var left = Abs(Console.CursorLeft - 1);
            var top = Console.CursorTop;

            ClearBox();

            WriteValue(' ', (Console.WindowWidth / 2) - (textLength / 2));

            writeText();

            WriteValue(' ', (Console.WindowWidth / 2) - (textLength / 2));

            SetCursor(top: top, left: left);
        }

        private static void ClearBox()
        {
            SetCursor(top: 2);

            WriteValue(' ', times: Console.WindowWidth);
            WriteValue(NewLine);
            WriteValue(' ', times: Console.WindowWidth);
            WriteValue(NewLine);
            WriteValue(' ', times: Console.WindowWidth);

            SetCursor(top: 3);
        }
    }
}
