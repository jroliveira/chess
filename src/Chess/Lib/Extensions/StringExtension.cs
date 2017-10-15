namespace Chess.Lib.Extensions
{
    using Chess.Entities;

    internal static class StringExtension
    {
        public static Position ToPosition(this string position)
        {
            var file = position[0];
            var rank = position[1];

            return new Position(file, rank);
        }
    }
}