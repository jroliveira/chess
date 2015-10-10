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
                System.Console.Write(" ");
            }
        }

        public void Write(string text)
        {
            System.Console.Write(text);
        }

        public void WriteError(string text)
        {
            var left = Math.Abs(System.Console.CursorLeft - 1);
            var top = System.Console.CursorTop;

            System.Console.SetCursorPosition(0, 10);
            System.Console.WriteLine("                                                                                   ");
            System.Console.WriteLine("                                                                                   ");

            var cursorLeft = (42 - (text.Length / 2));

            System.Console.SetCursorPosition(cursorLeft, 10);
            Pipe();
            Margin(2);

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Write(text);
            System.Console.ForegroundColor = ConsoleColor.White;

            Margin(2);
            Pipe();
            NewLine();
            System.Console.SetCursorPosition(cursorLeft - 3, 11);
            Divider(DividerPosition.Bottom, text.Length + 4);

            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.SetCursorPosition(left, top);
        }

        public void WriteOption(string option, string caption)
        {
            Divider(DividerPosition.Top, option.Length + 2);
            Margin();
            Pipe();
            Margin(1);

            System.Console.Write(option);

            Margin(1);
            Pipe();

            Margin(1);

            System.Console.Write(caption);

            NewLine();
            Divider(DividerPosition.Bottom, option.Length + 2);
        }

        public void WriteWithSleep(string format, params object[] args)
        {
            var text = string.Format(format, args);
            WriteWithSleep(text);
        }

        public virtual void WriteWithSleep(string text)
        {
            foreach (var t in text)
            {
                Thread.Sleep(60);
                System.Console.Write(t);
            }
        }

        public void WriteInsideTheBox(string text)
        {
            Divider(DividerPosition.Top, text.Length + 4);
            Margin();
            Pipe();
            Margin(2);

            System.Console.Write(text);

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
            System.Console.Write("   {0}", leftCorner);

            for (var j = 0; j < length; j++)
            {
                Dash();
            }

            System.Console.Write(rightCorner);

            NewLine();
        }

        private static void Dash()
        {
            System.Console.Write("═");
        }

        private static void Pipe()
        {
            System.Console.Write('║');
        }

        private static void Margin(int length = 3)
        {
            for (var i = 0; i < length; i++)
            {
                System.Console.Write(" ");
            }
        }
    }
}