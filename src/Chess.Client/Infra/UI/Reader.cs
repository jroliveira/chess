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

        internal static string ReadOption(Func<string, bool> condition, string errorMessage)
        {
            bool valid;
            string option;

            do
            {
                option = ReadText();
                valid = condition(option);

                if (!valid)
                {
                    WriteError(errorMessage);
                }
            }
            while (!valid);

            return option;
        }
    }
}
