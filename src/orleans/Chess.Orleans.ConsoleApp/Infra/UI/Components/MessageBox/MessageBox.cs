namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    using System;

    using Chess.Infra.Monad;

    using static System.ConsoleColor;
    using static System.String;
    using static System.Threading.Tasks.Task;

    using static Chess.Infra.Monad.Utils.Util;

    internal static partial class Terminal
    {
        internal static Unit ClearMessage() => WriteBox(Empty, DefaultBackgroundColor);

        internal static Unit ShowError(ChessException exception, bool confirm = false, bool clean = true) => ShowError(exception.Message, confirm, clean);

        internal static Unit ShowError(string error, bool confirm = false, bool clean = true) => ShowMessage(error, confirm, clean, DarkRed);

        internal static Unit ShowInfo(string info, bool confirm = false, bool clean = true) => ShowMessage(info, confirm, clean, DarkBlue);

        private static Unit ShowMessage(string message, bool confirm, bool clean, ConsoleColor backgroundColor)
        {
            if (confirm)
            {
                WriteBox($"{message}   [confirm]", backgroundColor);
                Console.ReadKey();
                ClearMessage();

                return Unit();
            }

            WriteBox(message, backgroundColor);

            if (clean)
            {
                Delay(1500).ContinueWith(_ => ClearMessage());
            }

            return Unit();
        }
    }
}
