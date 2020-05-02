namespace Chess.Orleans.Contract
{
    using Chess.Infra.Monad;

    using global::Orleans;

    public interface IUserGrain : IGrainObserver
    {
        void GameChanged(Try<Match> match);

        void YourMove(Try<Match> match, IMatchGrain matchGrain);
    }
}
