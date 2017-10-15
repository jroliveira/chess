namespace Chess.Terminal.Lib.Match
{
    using System;

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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void White()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}