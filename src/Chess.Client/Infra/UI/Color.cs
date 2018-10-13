namespace Chess.Client.Infra.UI
{
    using System;

    using static System.Console;
    using static System.ConsoleColor;

    internal static class Color
    {
        public static ConsoleColor CurrentBackgroundColor => BackgroundColor;

        public static void ApplyColor(bool secondColor = false)
        {
            if (secondColor)
            {
                BackgroundColor = White;
                ForegroundColor = Black;
            }
            else
            {
                BackgroundColor = Black;
                ForegroundColor = White;
            }
        }
    }
}
