using Chess.Multiplayer.Socket;

namespace Chess.Multiplayer.Test.Fakes
{
    internal class FakeMultiplayer : Multiplayer
    {
        public FakeMultiplayer(ISocket socket) 
            : base(socket)
        {

        }
    }
}