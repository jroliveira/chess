namespace Chess.Interfaces
{
    using System.Threading.Tasks;

    using Chess.Lib.Monad;

    using Orleans;

    public interface IGameServer : IGrainWithStringKey
    {
        Task Subscribe(IGameClient gameClient);

        Task<Try<Unit>> Start();

        Task<Try<Unit>> JoinPlayer(Option<string> playerName);

        Task<Try<Unit>> MovePiece(Option<string> piecePosition, Option<string> newPosition, Option<string> playerName);
    }
}
