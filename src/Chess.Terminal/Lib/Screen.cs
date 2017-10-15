namespace Chess.Terminal.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;

    using static System.Console;
    using static System.ConsoleColor;
    using static System.Text.Encoding;

    using static Chess.Terminal.Lib.Constants.Symbols;

    public class Screen : IScreen
    {
        static Screen() => OutputEncoding = GetEncoding(65001);

        public string Title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        public void ClearScreen()
        {
            Clear();
        }

        public void ClearOption()
        {
            SetCursorPosition(0, 22);

            Observable
                .Range(0, 40)
                .Subscribe(number => Write(' '));

            SetCursorPosition(0, 22);
        }

        public char GetChar()
        {
            return ReadKey().KeyChar;
        }

        public string GetString()
        {
            return ReadLine();
        }

        public void WriteError(string text)
        {
            var left = Math.Abs(CursorLeft - 1);
            var top = CursorTop;

            SetCursorPosition(0, 23);

            ForegroundColor = Red;
            Write("   {0}", text);
            ForegroundColor = White;

            SetCursorPosition(left, top);
        }

        public void WriteNewLine()
        {
            WriteLine();
        }

        public void WriteOption(char option, string caption)
        {
            Divider(DividerPosition.Top, option.ToString().Length + 2);
            Margin();
            Write(Box.Pipe);
            Margin(1);

            Write(option);

            Margin(1);
            Write(Box.Pipe);

            Margin(1);

            Write(caption);

            this.WriteNewLine();
            Divider(DividerPosition.Bottom, option.ToString().Length + 2);
            this.WriteNewLine();
        }

        public void WriteText(string text)
        {
            Write(text);
        }

        public void WriteText(char letter)
        {
            Write(letter);
        }

        public void WriteTitle(string text)
        {
            Divider(DividerPosition.Top, text.Length + 4);
            Margin();
            Write(Box.Pipe);
            Margin(2);

            Write(text);

            Margin(2);
            Write(Box.Pipe);

            this.WriteNewLine();
            Divider(DividerPosition.Bottom, text.Length + 4);
            this.WriteNewLine();
            this.WriteNewLine();
        }

        private static void Divider(DividerPosition position, int length)
        {
            var dividers = new Dictionary<DividerPosition, Action>
            {
                { DividerPosition.Top, () => Divider(Box.Upper.Left, Box.Upper.Right, length) },
                { DividerPosition.Bottom, () => Divider(Box.Bottom.Left, Box.Bottom.Right, length) }
            };

            dividers[position]();
        }

        private static void Divider(char leftCorner, char rightCorner, int length)
        {
            Write("   {0}", leftCorner);

            Observable
                .Range(0, length)
                .Subscribe(number => Write(Box.Dash));

            Write(rightCorner);
            WriteLine(string.Empty);
        }

        private static void Margin(int length = 3)
        {
            Observable
                .Range(0, length)
                .Subscribe(number => Write(' '));
        }
    }
}
