using System.Linq;

namespace Chess.Extensions
{
    public static class CharExtension
    {
        public static bool IsFirst(this char[] collection, char element)
        {
            return element.Equals(collection.First());
        }

        public static bool IsLast(this char[] collection, char element)
        {
            return element.Equals(collection.Last());
        }
    }
}