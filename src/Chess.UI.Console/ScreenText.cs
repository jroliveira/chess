using System;
using System.Threading;

namespace Chess.UI.Console
{
    public class ScreenText
    {
        private readonly ScreenColor _color;

        public ScreenText()
        {
            _color = new ScreenColor();
        }

        public void NewLine()
        {
            System.Console.WriteLine("");
        }

        public void Pipe()
        {
            var current = System.Console.BackgroundColor;

            _color.Restore();

            System.Console.Write("│");
            //System.Console.Write("║");

            if (current == ConsoleColor.White)
            {
                _color.White();
            }
        }

        public void Dash(int limit = 0)
        {
            //System.Console.WriteLine("  ───┼──────┼──────┼──────┼──────┼──────┼──────┼──────┼──────┼───");
            //System.Console.WriteLine("  ═══╬══════╬══════╬══════╬══════╬══════╬══════╬══════╬══════╬═══");

            switch (limit)
            {
                case -1:
                    System.Console.WriteLine("     ┌──────┬──────┬──────┬──────┬──────┬──────┬──────┬──────┐");
                    //System.Console.WriteLine("     ╔══════╦══════╦══════╦══════╦══════╦══════╦══════╦══════╗");
                    break;

                case 0:
                    System.Console.WriteLine("     ├──────┼──────┼──────┼──────┼──────┼──────┼──────┼──────┤");
                    //System.Console.WriteLine("     ╠══════╬══════╬══════╬══════╬══════╬══════╬══════╬══════╣");
                    break;

                case 1:
                    System.Console.WriteLine("     └──────┴──────┴──────┴──────┴──────┴──────┴──────┴──────┘");
                    //System.Console.WriteLine("     ╚══════╩══════╩══════╩══════╩══════╩══════╩══════╩══════╝");
                    break;
            }
        }

        public void Error(string message)
        {
            var left = Math.Abs(System.Console.CursorLeft - 1);
            var top = System.Console.CursorTop;

            System.Console.SetCursorPosition(left, top);
            System.Console.Write("");

            System.Console.ForegroundColor = ConsoleColor.Red;

            System.Console.SetCursorPosition(15, 38);
            System.Console.WriteLine("                                                 ");

            System.Console.SetCursorPosition(15, 38);
            System.Console.WriteLine(message);

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

        public void Write(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Write(text);
        }

        public void Write(string text)
        {
            foreach (var t in text)
            {
                Thread.Sleep(60);
                System.Console.Write(t);
            }
        }
    }
}