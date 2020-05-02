namespace System.Collections.Generic
{
    using Chess.Infra.Monad;

    public static class SetExtension
    {
        public static bool AddItem<T>(this ISet<T> @this, Option<T> newItem) => @this.Add(newItem.Get());
    }
}
