namespace System.Collections.Generic
{
    using System.Linq;

    using Chess.Infra.Monad;

    public static class CollectionExtension
    {
        public static void AddItem<T>(this ICollection<T> @this, Option<T> newItemOption) => newItemOption.ForEach(@this.Add);
    }
}
