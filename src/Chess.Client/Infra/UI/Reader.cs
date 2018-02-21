namespace Chess.Client.Infra.UI
{
    using static System.Console;

    internal static class Reader
    {
        public static char ReadChar() => ReadKey().KeyChar;
    }
}
