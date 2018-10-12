namespace Chess.Grains
{
    using System;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;
    using Chess.Lib.Monad.Extensions;

    using Orleans;

    using static System.Threading.Tasks.Task;
    using static Chess.Lib.Monad.Utils.Util;

    public class Match : Grain, IMatch
    {
        private const int NumberOfPlayers = 2;
        private ObserverSubscriptionManager<IPlayerCallback> players;
        private ObserverSubscriptionManager<IPlayerCallback> spectators;
        private IGame game;
        private IDealer dealer;

        public Task WakeUp() => Task.CompletedTask;

        public override Task OnActivateAsync()
        {
            this.dealer = this.GrainFactory.GetGrain<IDealer>(Guid.NewGuid());

            var registry = this.GrainFactory.GetGrain<IMatchRegistry>("match_registry");
            registry.AddMatch(this);

            this.game = new Game();
            this.players = new ObserverSubscriptionManager<IPlayerCallback>();
            this.spectators = new ObserverSubscriptionManager<IPlayerCallback>();

            return base.OnActivateAsync();
        }

        public Task<Try<Unit>> Start()
        {
            var chessboard = this.game.Start().GetOrElse(default);
            this.players.Notify(client => client.GameChanged(chessboard));

            return FromResult(Success(Unit()));
        }

        public Task<Try<Unit>> JoinPlayer(Option<string> playerName)
        {
            this.game.JoinPlayer(playerName);

            return FromResult(Success(Unit()));
        }

        public async Task<Try<Unit>> MoveAsync(Option<string> piecePosition, Option<string> newPosition, Option<string> playerName)
        {
            var chessboard = this.game.MovePiece(piecePosition, newPosition, playerName);

            this.players.Notify(client => client.GameChanged(chessboard));
            this.spectators.Notify(spectators => spectators.GameChanged(chessboard));

            var nextplayer = await this.dealer.NextPlayer();
            nextplayer.YourMove(this);

            return Success(Unit());
        }

        public async Task JoinAsync(IPlayerCallback player)
        {
            if (this.players.Count == NumberOfPlayers)
            {
                this.spectators.Subscribe(player);
            }

            this.players.Subscribe(player);
            await this.dealer.AddPlayer(player);

            if (this.players.Count == NumberOfPlayers)
            {
                (await this.dealer.NextPlayer()).YourMove(this);
            }
        }
    }
}
