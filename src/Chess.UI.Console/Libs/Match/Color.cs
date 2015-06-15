using System;

namespace Chess.UI.Console.Libs.Match
{
    public class Color
    {
        public void Toggle(bool toggle)
        {
            if (toggle)
            {
                White();
            }
            else
            {
                Restore();
            }
        }

        public void Restore()
        {
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.White;
        }

        public void White()
        {
            System.Console.BackgroundColor = ConsoleColor.White;
            System.Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}