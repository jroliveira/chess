namespace Chess.Client.Infra.UI
{
    using System;

    using static Chess.Client.Infra.UI.WriterBox;

    internal static class Writer
    {
        internal static void SetCursor(int top = 1, int left = 0) => Console.SetCursorPosition(left, top);

        internal static void ClearScreen()
        {
            Console.Clear();

            SetCursor(top: 3);
        }

        internal static void WriteError(string error) => WriteBox(error.Length, () =>
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteValue(error);
            Console.ForegroundColor = ConsoleColor.White;
        });

        internal static void WriteNewLine() => Console.WriteLine();

        internal static void WriteValue(object value) => Console.Write(value);

        internal static void WriteValue(string format, params object[] args) => Console.Write(format, args);

        internal static void WriteTextWithNewLine(string text) => Console.WriteLine(text);
    }
}
