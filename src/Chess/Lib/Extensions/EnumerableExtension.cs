namespace Chess.Lib.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;

    public static class EnumerableExtension
    {
        public static IDisposable Subscribe<TSource>(this IEnumerable<TSource> source, Action<TSource> onNext) => source
            .ToObservable()
            .Subscribe(onNext);

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> onNext) => source
            .ToList()
            .ForEach(onNext);
    }
}