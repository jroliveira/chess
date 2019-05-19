namespace Chess.Client.Scenarios.CreatePlayer
{
    using System.Threading.Tasks;

    using Chess.Client.Infra.Extensions;

    using static System.ConsoleColor;
    using static System.String;
    using static System.Threading.Tasks.Task;

    using static Chess.Client.Infra.UI.Terminal;

    internal static partial class CreatePlayerScenario
    {
        private const string InputLabel = " player's name: ";
        private const int InputLength = 23;
        private static readonly int InputArea = InputLabel.Length + InputLength;

        private static readonly (short Top, short Left) CursorStartPosition = (Top: 10, Left: 0);

        private static (string Left, string Text, string Right) DrawScenario()
        {
            SetCursorPosition(CursorStartPosition.Top, CursorStartPosition.Left);

            Write(' '.Repeat(InputArea));
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

        private static async Task PrintSuccessMessage()
        {
            WriteInfo("Player created.");
            await Delay(1500);
        }

        private static string GetPlayerName(int leftSpaceLength) => ReadText(
            () =>
            {
                ApplyScreenColor(background: DarkGray, foreground: Black);
                SetCursorPosition(top: CursorStartPosition.Top + 1, left: leftSpaceLength + InputLabel.Length);
            },
            value => !IsNullOrEmpty(value),
            "Invalid player's name.");
    }
}
