namespace Chess.Orleans.ConsoleApp.Infra.Extensions
{
    using static Chess.Orleans.ConsoleApp.Infra.Utils.Util;

    internal static class StringExtension
    {
        internal static (string Left, string Text, string Right) Center(this string @this, int areaLength) => CenterValue(@this, areaLength);
    }
}
