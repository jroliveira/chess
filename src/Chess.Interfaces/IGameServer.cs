namespace Chess.Interfaces
{
    using System.Threading.Tasks;
    using Chess.Lib.Monad;
    using Orleans;

    public interface IGameServer : IGrainWithStringKey
    {
        Task Subscribe(IGameClient gameClient);

        Task<Try<Unit>> Start();

        Task<Try<Unit>> Move(Option<string> piecePosition, Option<string> newPosition);
    }
}