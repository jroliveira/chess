namespace Chess.UI.Console.Libs
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using static System.Console;

    public class Writer : IWriter
    {
        public void NewLine()
        {
            WriteLine(string.Empty);
        }

        public void Erase()
        {
            for (var i = 0; i < 40; i++)
            {
                Write(' ');
            }
        }

        public void WriteError(string text)
        {
            var left = Math.Abs(CursorLeft - 1);
            var top = CursorTop;

            SetCursorPosition(0, 23);

            ForegroundColor = ConsoleColor.Red;
            Write("   {0}", text);
            ForegroundColor = ConsoleColor.White;

            SetCursorPosition(left, top);
        }

        public void WriteOption(string option, string caption)
        {
            this.Divider(DividerPosition.Top, option.Length + 2);
            Margin();
            Pipe();
            Margin(1);

            Write(option);

            Margin(1);
            Pipe();

            Margin(1);

            Write(caption);

            this.NewLine();
            this.Divider(DividerPosition.Bottom, option.Length + 2);
        }

        public virtual void WriteWithSleep(string text)
        {
            foreach (var t in text)
            {
                Thread.Sleep(60);
                Write(t);
            }
        }

        public void WriteInsideTheBox(string text)
        {
            this.Divider(DividerPosition.Top, text.Length + 4);
            Margin();
            Pipe();
            Margin(2);

            Write(text);

            Margin(2);
            Pipe();

            this.NewLine();
            this.Divider(DividerPosition.Bottom, text.Length + 4);
        }

        private static void Dash()
        {
            Write('═');
        }

        private static void Pipe()
        {
            Write('║');
        }

        private static void Margin(int length = 3)
        {
            for (var i = 0; i < length; i++)
            {
                Write(' ');
            }
        }

        private static void Write(char @char, params object[] args)
        {
            Write(new string(@char, 1), args);
        }

        private static void Write(string text, params object[] args)
        {
            Console.Write(text, args);
        }

        private void Divider(DividerPosition position, int length)
        {
            var dividers = new Dictionary<DividerPosition, Action>
            {
                { DividerPosition.Top,    () => this.Divider('╔', '╗', length) },
                { DividerPosition.Bottom, () => this.Divider('╚', '╝', length) }
            };

            dividers[position]();
        }

        private void Divider(char leftCorner, char rightCorner, int length)
        {
            Write("   {0}", leftCorner);

            for (var j = 0; j < length; j++)
            {
                Dash();
            }

            Write(rightCorner);

            this.NewLine();
        }
    }
}