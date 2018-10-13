namespace Chess.Grains
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Interfaces;

    using Orleans;

    using static System.Threading.Tasks.Task;

    public class MatchRegistry : Grain, IMatchRegistry
    {
        private readonly ICollection<IMatch> matches = new List<IMatch>();

        public Task AddMatch(IMatch match)
        {
            this.matches.Add(match);

            return CompletedTask;
        }

        public Task<IReadOnlyCollection<string>> GetMatches() => FromResult(new ReadOnlyCollection<string>(
            this.matches
                .Select(match => match.GetPrimaryKeyString())
                .ToList()) as IReadOnlyCollection<string>);
    }
}
