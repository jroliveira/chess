namespace Chess.Client.Infra.UI
{
    using System;

    internal static partial class Terminal
    {
        internal static string ReadText(
            Action setup,
            Func<string, bool> condition,
            string errorMessage)
        {
            bool valid;
            string text;

            do
            {
                setup();
                text = Console.ReadLine();
                valid = condition(text);

                if (!valid)
                {
                    WriteError(errorMessage);
                }

                ResetScreenColor();
            }
            while (!valid);

            return text;
        }

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
                    WriteError(errorMessage);
                }

                ResetScreenColor();
            }
            while (!valid);

            return @char;
        }
    }
}
