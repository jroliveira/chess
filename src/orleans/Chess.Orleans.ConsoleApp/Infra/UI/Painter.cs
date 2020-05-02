namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    using System;

    using Chess.Infra.Monad;

    using static System.ConsoleColor;

    using static Chess.Infra.Monad.Utils.Util;

    internal static partial class Terminal
    {
        internal static ConsoleColor DefaultBackgroundColor => Gray;

        internal static Unit ResetScreenColor()
        {
            Console.BackgroundColor = DefaultBackgroundColor;
            Console.ForegroundColor = White;

            return Unit();
        }

        internal static Unit ApplyScreenColor(ConsoleColor background = Gray, ConsoleColor foreground = White)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;

            return Unit();
        }

        internal static Unit ApplyBoardColor(bool isWhiteColor = false)
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

            return Unit();
        }
    }
}
