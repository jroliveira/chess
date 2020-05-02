namespace System.Collections.Generic
{
    using System.Linq;

    public static class ReadOnlyCollectionExtension
    {
        public static IReadOnlyCollection<T> RemoveItem<T>(this IReadOnlyCollection<T> @this, T item) => new List<T>(@this.Where(current => current != null && !current.Equals(item)));

        public static IReadOnlyCollection<T> AddItem<T>(this IReadOnlyCollection<T> @this, T newItem) => new List<T>(@this) { newItem };
    }
}
