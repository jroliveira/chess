namespace Chess
{
    public static class StringExtension
    {
        public static Position ToPosition(this string position)
        {
            var file = position[0];
            var rawn = position[1];

            return new Position(file, rawn);
        }
    }
}