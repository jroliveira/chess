namespace Chess.Lib.Extensions
{
    using Chess.Entities;

    using static System.Char;
    using static System.Convert;

    internal static class StringExtension
    {
        public static Position ToPosition(this string position)
        {
            var file = position[0];
            var rank = ToUInt32(GetNumericValue(position[1]));

            return new Position(file, rank);
        }
    }
}
