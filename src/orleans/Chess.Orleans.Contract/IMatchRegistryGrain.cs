namespace Chess.Orleans.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chess.Infra.Monad;

    using global::Orleans;

    public interface IMatchRegistryGrain : IGrainWithStringKey
    {
        Task<Unit> AddMatchGrain(IMatchGrain matchGrain);

        Task<IReadOnlyCollection<string>> GetMatches();
    }
}
