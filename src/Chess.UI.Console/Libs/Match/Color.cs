namespace Chess.UI.Console.Libs.Match
{
    using System;

    using static System.Console;

    public class Color
    {
        public void Toggle(bool toggle)
        {
            if (toggle)
            {
                this.White();
            }
            else
            {
                this.Restore();
            }
        }

        public void Restore()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
        }

        public void White()
        {
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Black;
        }
    }
}