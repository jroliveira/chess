namespace Chess.Orleans.Grain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Infra.Monad;
    using Chess.Orleans.Contract;

    using global::Orleans;

    using static Chess.Infra.Monad.Utils.Util;

    public class MatchRegistryGrain : Grain, IMatchRegistryGrain
    {
        private readonly ICollection<IMatchGrain> matchesGrain = new List<IMatchGrain>();

        public Task<Unit> AddMatchGrain(IMatchGrain matchGrain)
        {
            this.matchesGrain.Add(matchGrain);

            return Task(Unit());
        }

        public Task<IReadOnlyCollection<string>> GetMatches() => Task<IReadOnlyCollection<string>>(this.matchesGrain
            .Select(matchGrain => matchGrain.GetPrimaryKeyString())
            .ToList());
    }
}
