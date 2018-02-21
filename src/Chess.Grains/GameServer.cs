namespace Chess.Grains
{
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;
    using Chess.Lib.Monad.Extensions;

    using Orleans;

    using static System.Threading.Tasks.Task;
    using static Chess.Lib.Monad.Utils.Util;

    public class GameServer : Grain, IGameServer
    {
        private IGame game;
        private ObserverSubscriptionManager<IGameClient> subscriptions;

        public override Task OnActivateAsync()
        {
            this.game = new Game();
            this.subscriptions = new ObserverSubscriptionManager<IGameClient>();

            return base.OnActivateAsync();
        }

        public Task Subscribe(IGameClient gameClient)
        {
            this.subscriptions.Subscribe(gameClient);
            return CompletedTask;
        }

        public Task<Try<Unit>> Start()
        {
            var chessboard = this.game.Start();
            this.subscriptions.Notify(client => client.GameChanged(chessboard, this));

            return FromResult(Success(Unit()));
        }

        public Task<Try<Unit>> JoinPlayer(Option<string> playerName)
        {
            this.game.JoinPlayer(playerName);

            return FromResult(Success(Unit()));
        }

        public Task<Try<Unit>> MovePiece(Option<string> piecePosition, Option<string> newPosition, Option<string> playerName)
        {
            var chessboard = this.game.MovePiece(piecePosition, newPosition, playerName);
            this.subscriptions.Notify(client => client.GameChanged(chessboard, this));

            return FromResult(Success(Unit()));
        }
    }
}
