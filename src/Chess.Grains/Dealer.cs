namespace Chess.Grains
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Chess.Interfaces;
    using Orleans;

    public class Dealer : Grain, IDealer
    {
        private bool IsFirst;
        private readonly List<IPlayerCallback> _players = new List<IPlayerCallback>();

        public Dealer() => this.IsFirst = false;

        public Task<IPlayerCallback> NextPlayer()
        {
            this.IsFirst = !this.IsFirst;

            if (this.IsFirst)
            {
                return Task.FromResult(this._players.First());
            }

            return Task.FromResult(this._players.ElementAt(1));
        }

        public Task AddPlayer(IPlayerCallback player)
        {
            this._players.Add(player);
            return Task.CompletedTask;
        }
    }
}
