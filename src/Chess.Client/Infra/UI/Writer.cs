namespace Chess.Client.Infra.UI
{
    using System;
    using System.Collections.Generic;
    using static Chess.Client.Infra.UI.Color;
    using static Chess.Client.Infra.UI.Symbols;
    using static System.Math;

    internal static class Writer
    {
        private static readonly IReadOnlyDictionary<DividerPosition, Action> Dividers = new Dictionary<DividerPosition, Action>
        {
            { DividerPosition.Top, () => WriteDivider(Board.Upper.Left, Board.Upper.Right, Board.Upper.Center) },
            { DividerPosition.Middle, () => WriteDivider(Board.Middle.Left, Board.Middle.Right, Board.Middle.Center) },
            { DividerPosition.Bottom, () => WriteDivider(Board.Bottom.Left, Board.Bottom.Right, Board.Bottom.Center) },
        };

        public static void SetCursor(int top = 1, int left = 0) => Console.SetCursorPosition(left, top);

        public static void WriteDivider(DividerPosition position) => Dividers[position]();

        public static void WritePipe()
        {
            var backgroundColor = CurrentBackgroundColor;

            ApplyColor();

            WriteValue(Board.Pipe);

            if (backgroundColor == ConsoleColor.White)
            {
                ApplyColor(secondColor: true);
            }
        }

        public static void WriteError(string error)
        {
            var left = Abs(Console.CursorLeft - 1);
            var top = Console.CursorTop;

            SetCursor(top: 23);

            Console.ForegroundColor = ConsoleColor.Red;
            WriteValue("   {0}", error);
            Console.ForegroundColor = ConsoleColor.White;

            SetCursor(top: top, left: left);
        }

        public static void WriteNewLine() => Console.WriteLine();

        public static void WriteValue(object value) => Console.Write(value);

        public static void WriteValue(string format, params object[] args) => Console.Write(format, args);

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
    }
}