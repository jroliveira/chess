using System;
using System.Collections.Generic;
using System.Threading;

namespace Chess.UI.Console.Libs
{
    public class Writer : IWriter
    {
        public void NewLine()
        {
            System.Console.WriteLine("");
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
            var left = Math.Abs(System.Console.CursorLeft - 1);
            var top = System.Console.CursorTop;

            System.Console.SetCursorPosition(0, 23);

            System.Console.ForegroundColor = ConsoleColor.Red;
            Write("   {0}", text);
            System.Console.ForegroundColor = ConsoleColor.White;

            System.Console.SetCursorPosition(left, top);
        }

        public void WriteOption(string option, string caption)
        {
            Divider(DividerPosition.Top, option.Length + 2);
            Margin();
            Pipe();
            Margin(1);

            Write(option);

            Margin(1);
            Pipe();

            Margin(1);

            Write(caption);

            NewLine();
            Divider(DividerPosition.Bottom, option.Length + 2);
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
            Divider(DividerPosition.Top, text.Length + 4);
            Margin();
            Pipe();
            Margin(2);

            Write(text);

            Margin(2);
            Pipe();

            NewLine();
            Divider(DividerPosition.Bottom, text.Length + 4);
        }

        private void Divider(DividerPosition position, int length)
        {
            var dividers = new Dictionary<DividerPosition, Action>
            {
                { DividerPosition.Top,    () => Divider('╔', '╗', length) },
                { DividerPosition.Bottom, () => Divider('╚', '╝', length) }
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

            NewLine();
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
            System.Console.Write(text, args);
        }
    }
}