namespace Chess.Lib
{
    using System;

    public class Observer<T> : IObserver<T>
    {
        private IDisposable unsubscriber;

        public event Action<T> Updated;

        public static Observer<T> Observe(IObservable<T> observable)
        {
            var observer = new Observer<T>();
            observer.Subscribe(observable);

            return observer;
        }

        public void OnCompleted()
        {
            this.Unsubscribe();
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            this.Updated?.Invoke(value);
        }

        public void Subscribe(IObservable<T> observable)
        {
            if (observable != null)
            {
                this.unsubscriber = observable.Subscribe(this);
            }
        }

        public void Unsubscribe()
        {
            this.unsubscriber.Dispose();
        }
    }
}