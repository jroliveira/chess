namespace Chess.Client.Infra.UI
{
    using System;

    using Chess.Client.Infra.Extensions;

    using static System.ConsoleColor;
    using static System.Math;
    using static System.String;

    using static Chess.Client.Infra.Utils.Util;

    internal static partial class Terminal
    {
        internal static void SetCursorPosition(int top = 1, int left = 0) => Console.SetCursorPosition(left, top);

        internal static void ClearMsg() => WriteBox(Empty, DefaultBackgroundColor);

        internal static void WriteError(string error) => WriteBox(error, DarkRed);

        internal static void WriteInfo(string info) => WriteBox(info, DarkBlue);

        internal static void WriteValue(object value) => Console.Write(value);

        internal static void WriteValue(object value, int times) => WriteValue(RepeatValue(value, times));

        internal static void WriteValues((string, string, string) values)
        {
            WriteValue(values.Item1);
            WriteValue(values.Item2);
            WriteValue(values.Item3);
        }

        private static void WriteBox(string text, ConsoleColor backgroundColor)
        {
            var cursorLeft = Abs(Console.CursorLeft - 1);
            var cursorTop = Console.CursorTop;

            SetCursorPosition(top: 2);
            ApplyScreenColor(background: backgroundColor);

            WriteValue(' '.Repeat(ScreenWidth));
            WriteValues(text.Center(ScreenWidth));
            WriteValue(' '.Repeat(ScreenWidth));

            SetCursorPosition(top: cursorTop, left: cursorLeft);
            ResetScreenColor();
        }
    }
}
