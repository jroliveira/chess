namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    using System;

    internal static partial class Terminal
    {
        internal static char ReadChar(
            Action setup,
            Func<char, bool> condition,
            string errorMessage)
        {
            bool valid;
            char @char;

            do
            {
                setup();
                @char = Console.ReadKey().KeyChar;
                valid = condition(@char);

                if (!valid)
                {
                    ShowError(errorMessage);
                }

                ResetScreenColor();
            }
            while (!valid);

            return @char;
        }
    }
}
