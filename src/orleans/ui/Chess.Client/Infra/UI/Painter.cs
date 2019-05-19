namespace Chess.Client.Infra.UI
{
    using System;

    using static System.ConsoleColor;

    internal static partial class Terminal
    {
        internal static ConsoleColor DefaultBackgroundColor => Gray;

        internal static ConsoleColor CurrentBackgroundColor => Console.BackgroundColor;

        internal static void ResetScreenColor()
        {
            Console.BackgroundColor = DefaultBackgroundColor;
            Console.ForegroundColor = White;
        }

        internal static void ApplyScreenColor(ConsoleColor background = Gray, ConsoleColor foreground = White)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }

        internal static void ApplyBoardColor(bool isWhiteColor = false)
        {
            if (isWhiteColor)
            {
                Console.BackgroundColor = White;
                Console.ForegroundColor = DarkGray;
            }
            else
            {
                Console.BackgroundColor = DarkGray;
                Console.ForegroundColor = White;
            }
        }
    }
}
