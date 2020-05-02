namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    using System;
    using System.Linq;

    using Chess.Orleans.ConsoleApp.Infra.Extensions;

    using static System.ConsoleColor;
    using static System.Convert;
    using static System.Int32;

    internal static partial class Terminal
    {
        internal static int GetFromSelectBox(string label, SelectOptions options)
        {
            var area = label.Length + InputLength;

            foreach (var (key, value) in options)
            {
                if (key < 0)
                {
                    WriteBoxLine(' '.Repeat(area));
                    continue;
                }

                WriteBoxLine($" {key} - {value}{' '.Repeat(area - (5 + value.Length))}");
            }

            var (left, _, _) = WriteBoxLine($"{label}{' '.Repeat(InputLength)}");
            WriteBoxLine(' '.Repeat(area));

            bool valid;
            char @char;

            do
            {
                ApplyScreenColor(background: DarkGray, foreground: Black);
                SetCursorPosition(top: 10 + options.Count, left: left.Length + label.Length);

                @char = Console.ReadKey().KeyChar;
                valid = TryParse(@char.ToString(), out var option) && options.Keys.ToList().Contains(option);

                if (!valid)
                {
                    ShowError("Invalid option");
                }

                ResetScreenColor();
            }
            while (!valid);

            return ToInt32(@char.ToString());
        }
    }
}
