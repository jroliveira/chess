namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    using System;

    using Chess.Infra.Monad;
    using Chess.Orleans.ConsoleApp.Infra.Extensions;

    using static System.ConsoleColor;
    using static System.Text.Encoding;

    using static Chess.Orleans.ConsoleApp.Infra.Win32.Win32ApiGateway;

    internal static partial class Terminal
    {
        private const string GameTitle = "CHESS";

        internal static int ScreenWidth => Console.WindowWidth;

        internal static Unit SetupScreen()
        {
            Console.Title = GameTitle;
            Console.OutputEncoding = GetEncoding(65001);
            Console.SetWindowSize(width: 54, height: 40);
            Console.SetBufferSize(width: 54, height: 40);
            DisableWindowResize();

            return ClearScreen();
        }

        internal static Unit ClearScreen()
        {
            ResetScreenColor();

            Console.Clear();

            ApplyScreenColor(background: White, foreground: DarkGray);

            WriteValues(GameTitle.Center(ScreenWidth));
            WriteValue(' '.Repeat(ScreenWidth));

            ResetScreenColor();

            return SetCursorPosition(top: 5);
        }
    }
}
