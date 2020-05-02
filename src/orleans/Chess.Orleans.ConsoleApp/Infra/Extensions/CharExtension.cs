namespace Chess.Orleans.ConsoleApp.Infra.Extensions
{
    using static Chess.Orleans.ConsoleApp.Infra.Utils.Util;

    internal static class CharExtension
    {
        internal static string Repeat(this char @this, int times) => RepeatValue(@this, times);
    }
}
