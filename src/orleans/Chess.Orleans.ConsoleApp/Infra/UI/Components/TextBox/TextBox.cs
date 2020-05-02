namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    using System;

    using Chess.Orleans.ConsoleApp.Infra.Extensions;

    using static System.ConsoleColor;
    using static System.String;

    internal static partial class Terminal
    {
        internal static string GetFromTextBox(string label)
        {
            var area = label.Length + InputLength;

            WriteBoxLine(' '.Repeat(area));
            var (left, _, _) = WriteBoxLine($"{label}{' '.Repeat(InputLength)}");
            WriteBoxLine(' '.Repeat(area));

            bool valid;
            string text;

            do
            {
                ApplyScreenColor(background: DarkGray, foreground: Black);
                SetCursorPosition(top: 11, left: left.Length + label.Length);

                text = Console.ReadLine();
                valid = !IsNullOrEmpty(text);

                if (!valid)
                {
                    ShowError("Invalid input");
                }

                ResetScreenColor();
            }
            while (!valid);

            return text;
        }
    }
}
