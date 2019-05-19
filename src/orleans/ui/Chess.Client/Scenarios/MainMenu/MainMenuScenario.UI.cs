namespace Chess.Client.Scenarios.MainMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Client.Infra.Extensions;

    using static System.ConsoleColor;
    using static System.Convert;
    using static System.Int32;

    using static Chess.Client.Infra.UI.Terminal;

    internal static partial class MainMenuScenario
    {
        private const string InputLabel = " menu option: ";
        private const int InputLength = 23;
        private static readonly int InputArea = InputLabel.Length + InputLength;

        private static readonly (short Top, short Left) CursorStartPosition = (Top: 12, Left: 0);

        private static (string Left, string Text, string Right) DrawScenario(IReadOnlyDictionary<int, string> menu)
        {
            SetCursorPosition(CursorStartPosition.Top, CursorStartPosition.Left);

            foreach (var item in menu)
            {
                if (item.Key < 0)
                {
                    Write(' '.Repeat(InputArea));
                    continue;
                }

                Write($" {item.Key} - {item.Value}{' '.Repeat(InputArea - (5 + item.Value.Length))}");
            }

            var @return = Write($"{InputLabel}{' '.Repeat(InputLength)}");
            Write(' '.Repeat(InputArea));

            return @return;

            (string Left, string Text, string Right) Write(string text)
            {
                var value = text.Center(ScreenWidth);

                WriteValue(value.Left);
                ApplyScreenColor(background: DarkGray);
                WriteValue(value.Text);
                ResetScreenColor();
                WriteValue(value.Right);

                return value;
            }
        }

        private static int GetOption(int leftSpaceLength, IReadOnlyDictionary<int, string> menu) => ToInt32(ReadChar(
            () =>
            {
                ApplyScreenColor(background: DarkGray, foreground: Black);
                SetCursorPosition(top: CursorStartPosition.Top + menu.Count + 1, left: leftSpaceLength + InputLabel.Length);
            },
            value => TryParse(value.ToString(), out var option) && menu.Keys.ToList().Contains(option),
            "Invalid option.").ToString());
    }
}
