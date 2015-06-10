using System;
using System.Collections.Generic;
using System.Threading;

namespace Chess.UI.Console.Libs
{
    public class Text
    {
        public void Error(string message)
        {
            var left = Math.Abs(System.Console.CursorLeft - 1);
            var top = System.Console.CursorTop;

            System.Console.SetCursorPosition(0, 0);
            System.Console.WriteLine("                                                                                   ");
            System.Console.WriteLine("                                                                                   ");

            var cursorLeft = (42 - (message.Length / 2));
            cursorLeft = cursorLeft % 2 != 0 ? cursorLeft + 1 : cursorLeft + 0;

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.SetCursorPosition(cursorLeft, 0);
            Pipe();

            System.Console.BackgroundColor = ConsoleColor.Red;
            Margin(2);
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(message);
            System.Console.ForegroundColor = ConsoleColor.Red;
            Margin(2);
            System.Console.BackgroundColor = ConsoleColor.Black;

            
            Pipe();
            NewLine();
            System.Console.SetCursorPosition(cursorLeft - 3, 1);
            Divider(DividerPosition.Bottom, message.Length + 4);

            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.SetCursorPosition(left, top);
        }

        public void Info(string message)
        {
            var left = Math.Abs(System.Console.CursorLeft - 1);
            var top = System.Console.CursorTop;

            System.Console.SetCursorPosition(15, 38);
            System.Console.WriteLine("                                                 ");

            System.Console.SetCursorPosition(15, 38);
            System.Console.WriteLine(message);

            System.Console.SetCursorPosition(left, top);
        }

        public void Write(string text)
        {
            System.Console.Write(text);
        }

        public void WriteWithSleep(string format, params object[] args)
        {
            var text = string.Format(format, args);
            WriteWithSleep(text);
        }

        public void WriteWithSleep(string text)
        {
            foreach (var t in text)
            {
                Thread.Sleep(60);
                System.Console.Write(t);
            }
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

        public void NewLine()
        {
            System.Console.WriteLine("");
        }

        public virtual void Dash()
        {
            System.Console.Write("═");
        }

        public virtual void Pipe()
        {
            System.Console.Write('║');
        }

        public void Margin(int length = 3)
        {
            for (var i = 0; i < length; i++)
            {
                System.Console.Write(" ");
            }
        }
    }
}