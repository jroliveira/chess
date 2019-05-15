namespace Chess.Client.Infra.UI
{
    using System;

    using static System.Console;
    using static System.Convert;

    using static Chess.Client.Infra.UI.Writer;

    internal static class Reader
    {
        internal static char ReadChar() => ReadKey().KeyChar;

        internal static string ReadText() => ReadLine();

        internal static int ReadNumber() => ToInt32(ReadLine());

        internal static string ReadText(Action setup, Func<string, bool> condition, string errorMessage)
        {
            bool valid;
            string text;

            do
            {
                setup();
                text = ReadText();
                valid = condition(text);

                if (!valid)
                {
                    WriteError(errorMessage);
                }
            }
            while (!valid);

            return text;
        }

        internal static char ReadChar(Action setup, Func<char, bool> condition, string errorMessage)
        {
            bool valid;
            char @char;

            do
            {
                setup();
                @char = ReadChar();
                valid = condition(@char);

                if (!valid)
                {
                    WriteError(errorMessage);
                }
            }
            while (!valid);

            return @char;
        }
    }
}
