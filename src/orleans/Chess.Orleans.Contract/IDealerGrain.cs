namespace Chess.Orleans.Contract
{
    using System.Threading.Tasks;

    using Chess.Infra.Monad;

    using global::Orleans;

    public interface IDealerGrain : IGrainWithGuidKey
    {
        Task<bool> HasBegun();

        Task<Try<IUserGrain>> NextUserGrain();

        Task<Try<Unit>> AddUserGrain(Option<IUserGrain> userGrainOption);
    }
}
