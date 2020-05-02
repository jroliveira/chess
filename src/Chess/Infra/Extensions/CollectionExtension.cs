namespace System.Collections.Generic
{
    using Chess.Infra.Monad;

    public static class CollectionExtension
    {
        public static void AddItem<T>(this ICollection<T> @this, Option<T> newItem) => @this.Add(newItem.Get());
    }
}
