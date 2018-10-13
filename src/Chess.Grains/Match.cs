namespace Chess.Grains
{
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    using static System.Guid;
    using static System.Threading.Tasks.Task;

    using static Chess.Lib.Monad.Utils.Util;

    public class Match : Grain, IMatch
    {
        private const int NumberOfPlayers = 2;
        private ObserverSubscriptionManager<IPlayer> players;
        private ObserverSubscriptionManager<IPlayer> spectators;
        private IGame game;
        private IDealer dealer;

        public Task WakeUp() => CompletedTask;

        public override Task OnActivateAsync()
        {
            this.dealer = this.GrainFactory.GetGrain<IDealer>(NewGuid());

            var registry = this.GrainFactory.GetGrain<IMatchRegistry>("match_registry");
            registry.AddMatch(this);

            this.game = new Game();
            this.players = new ObserverSubscriptionManager<IPlayer>();
            this.spectators = new ObserverSubscriptionManager<IPlayer>();

            return base.OnActivateAsync();
        }

        public Task<Try<Unit>> Start()
        {
            var chessboard = this.game.Start();
            this.players.Notify(client => client.GameChanged(chessboard));

            return FromResult(Success(Unit()));
        }

        public async Task<Try<Unit>> MovePiece(Option<string> piecePosition, Option<string> newPosition, Option<string> playerName)
        {
            var chessboard = this.game.MovePiece(piecePosition, newPosition, playerName);

            this.players.Notify(client => client.GameChanged(chessboard));
            this.spectators.Notify(spectator => spectator.GameChanged(chessboard));

            var nextPlayer = await this.dealer.NextPlayer();
            nextPlayer.YourMove(this);

            return Success(Unit());
        }

        public async Task<Try<Unit>> JoinPlayer(IPlayer player, Option<string> playerName)
        {
            player.SetName(playerName);
            this.game.JoinPlayer(playerName);

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

            return Success(Unit());
        }
    }
}
