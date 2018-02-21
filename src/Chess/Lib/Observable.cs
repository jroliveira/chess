namespace Chess.Lib
{
    using System;
    using System.Collections.Generic;
    using Chess.Lib.Extensions;

    public class Observable<T> : IObservable<T>
    {
        private readonly ICollection<IObserver<T>> observers;

        public Observable() => this.observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!this.observers.Contains(observer))
            {
                this.observers.Add(observer);
            }

            return new Unsubscriber(this.observers, observer);
        }

        protected void OnUpdate(T value) => this.observers.Subscribe(observer => observer.OnNext(value));

        private class Unsubscriber : IDisposable
        {
            private readonly ICollection<IObserver<T>> observers;
            private readonly IObserver<T> observer;

            public Unsubscriber(ICollection<IObserver<T>> observers, IObserver<T> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (this.observer != null && this.observers.Contains(this.observer))
                {
                    this.observers.Remove(this.observer);
                }
            }
        }
    }
}