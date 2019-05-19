namespace Chess.Client.Infra.Extensions
{
    using static Chess.Client.Infra.Utils.Util;

    internal static class CharExtension
    {
        internal static string Repeat(this char @this, int times) => RepeatValue(@this, times);
    }
}
