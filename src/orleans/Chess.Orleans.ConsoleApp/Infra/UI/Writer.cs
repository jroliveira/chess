namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    using System;

    using Chess.Infra.Monad;
    using Chess.Orleans.ConsoleApp.Infra.Extensions;

    using static System.ConsoleColor;
    using static System.Math;

    using static Chess.Infra.Monad.Utils.Util;

    internal static partial class Terminal
    {
        private const int InputLength = 23;

        internal static Unit SetCursorPosition(int top = 1, int left = 0)
        {
            Console.SetCursorPosition(left, top);

            return Unit();
        }

        internal static Unit WriteValue(object value)
        {
            Console.Write(value);

            return Unit();
        }

        internal static Unit WriteValues((string, string, string) values)
        {
            WriteValue(values.Item1);
            WriteValue(values.Item2);

            return WriteValue(values.Item3);
        }

        internal static (string Left, string Text, string Right) WriteTextBox(string label)
        {
            var area = label.Length + InputLength;

            WriteBoxLine(' '.Repeat(area));
            var @return = WriteBoxLine($"{label}{' '.Repeat(InputLength)}");
            WriteBoxLine(' '.Repeat(area));

            return @return;
        }

        internal static (string Left, string Text, string Right) WriteBoxLine(string text)
        {
            var value = text.Center(ScreenWidth);

            WriteValue(value.Left);
            ApplyScreenColor(background: DarkGray);
            WriteValue(value.Text);
            ResetScreenColor();
            WriteValue(value.Right);

            return value;
        }

        private static Unit WriteBox(string text, ConsoleColor backgroundColor)
        {
            var cursorLeft = Abs(Console.CursorLeft);
            var cursorTop = Console.CursorTop;

            SetCursorPosition(top: 2);
            ApplyScreenColor(background: backgroundColor);

            WriteValue(' '.Repeat(ScreenWidth));
            WriteValues(text.Center(ScreenWidth));
            WriteValue(' '.Repeat(ScreenWidth));

            SetCursorPosition(top: cursorTop, left: cursorLeft);

            return ResetScreenColor();
        }
    }
}
