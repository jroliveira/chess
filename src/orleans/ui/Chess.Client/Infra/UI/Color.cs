namespace Chess.Client.Infra.UI
{
    using System;

    using static System.Console;
    using static System.ConsoleColor;

    internal static class Color
    {
        internal static ConsoleColor CurrentBackgroundColor => BackgroundColor;

        internal static void ResetColor() => BackgroundColor = Black;

        internal static void ApplyColor(ConsoleColor color) => BackgroundColor = color;

        internal static void ApplyColor(bool secondColor = false)
        {
            if (secondColor)
            {
                BackgroundColor = White;
                ForegroundColor = DarkGray;
            }
            else
            {
                BackgroundColor = DarkGray;
                ForegroundColor = White;
            }
        }
    }
}
