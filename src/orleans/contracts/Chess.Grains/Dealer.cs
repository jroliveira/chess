namespace Chess.Grains
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Interfaces;

    using Orleans;

    using static System.Threading.Tasks.Task;

    public class Dealer : Grain, IDealer
    {
        private readonly ICollection<IPlayer> players = new List<IPlayer>();
        private bool isFirst;

        public Task<IPlayer> NextPlayer()
        {
            this.isFirst = !this.isFirst;

            return FromResult(this.isFirst
                ? this.players.First()
                : this.players.ElementAt(1));
        }

        public Task AddPlayer(IPlayer player)
        {
            this.players.Add(player);

            return CompletedTask;
        }
    }
}
