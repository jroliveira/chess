namespace Chess.Client.Infra.UI
{
    using System;

    using static System.ConsoleColor;

    using static Chess.Client.Infra.UI.Color;
    using static Chess.Client.Infra.UI.WriterBox;

    internal static class Writer
    {
        internal static void SetCursor(int top = 1, int left = 0) => Console.SetCursorPosition(left, top);

        internal static void ClearScreen()
        {
            Console.Clear();
            ApplyColor(White);

            Console.ForegroundColor = DarkGray;
            WriteValue(' ', times: (Console.WindowWidth / 2) - 3);
            WriteValue("CHESS");
            WriteValue(' ', times: (Console.WindowWidth / 2) - 2);
            WriteValue(' ', times: Console.WindowWidth);
            Console.ForegroundColor = White;

            ResetColor();
            SetCursor(top: 5);
        }

        internal static void WriteError(string error)
        {
            ApplyColor(DarkRed);

            WriteBox(error.Length, () =>
            {
                Console.ForegroundColor = White;
                WriteValue(error);
            });

            ResetColor();
        }

        internal static void WriteInfo(string info)
        {
            ApplyColor(DarkBlue);

            WriteBox(info.Length, () =>
            {
                Console.ForegroundColor = White;
                WriteValue(info);
            });

            ResetColor();
        }

        internal static void WriteValue(object value) => Console.Write(value);

        internal static void WriteValue(object value, int times)
        {
            for (var time = 0; time < times; time++)
            {
                WriteValue(value);
            }
        }
    }
}
