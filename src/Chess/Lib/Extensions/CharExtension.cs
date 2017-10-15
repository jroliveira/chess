namespace Chess.Lib.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class CharExtension
    {
        public static bool IsLast(this IReadOnlyCollection<char> collection, char element)
        {
            return element.Equals(collection.Last());
        }
    }
}