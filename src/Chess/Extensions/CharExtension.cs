namespace Chess.Extensions
{
    using System.Linq;

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