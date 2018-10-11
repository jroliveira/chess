namespace Chess.Grains
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Chess.Interfaces;
    using Orleans;

    public class MatchRegistry : Grain, IMatchRegistry
    {
        private readonly List<IMatch> registry = new List<IMatch>();

        public Task AddMatch(IMatch match)
        {
            this.registry.Add(match);
            return Task.CompletedTask;
        }

        public Task<List<IMatch>> GetAllMatches() => Task.FromResult(this.registry);
    }
}
