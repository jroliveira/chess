namespace Chess.Client.Infra.UI
{
    using System;

    using Chess.Client.Infra.Extensions;

    using static System.ConsoleColor;
    using static System.Text.Encoding;

    using static Chess.Client.Infra.Win32.Win32Gateway;

    internal static partial class Terminal
    {
        private const string GameTitle = "CHESS";

        internal static int ScreenWidth => Console.WindowWidth;

        internal static int ScreenHeight => Console.WindowHeight;

        internal static void SetupScreen()
        {
            Console.Title = GameTitle;
            Console.OutputEncoding = GetEncoding(65001);
            Console.SetWindowSize(width: 54, height: 38);
            Console.SetBufferSize(width: 54, height: 38);
            DisableWindowResize();
            ClearScreen();
        }

        internal static void ClearScreen()
        {
            ResetScreenColor();

            Console.Clear();

            ApplyScreenColor(background: White, foreground: DarkGray);

            WriteValues(GameTitle.Center(ScreenWidth));
            WriteValue(' '.Repeat(ScreenWidth));

            ResetScreenColor();
            SetCursorPosition(top: 5);
        }
    }
}
