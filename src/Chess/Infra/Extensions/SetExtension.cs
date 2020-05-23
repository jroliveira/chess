namespace System.Collections.Generic
{
    using System.Linq;

    using Chess.Infra.Monad;

    public static class SetExtension
    {
        public static bool AddItem<T>(this ISet<T> @this, Option<T> newItemOption) => newItemOption
            .Fold(false)(@this.Add);
    }
}
