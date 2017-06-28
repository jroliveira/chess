namespace Chess.Multiplayer.Socket
{
    using System.Net;

    internal interface ISocket
    {
        void Connect(IPAddress ipAddress, int port);

        int Send(string message);

        string Receive();

        void Shutdown();

        ISocket Accept();

        void Listen();

        void Bind(IPAddress ipAddress, int port);
    }
}