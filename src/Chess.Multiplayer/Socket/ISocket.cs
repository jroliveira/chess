using System.Net;

namespace Chess.Multiplayer.Socket
{
    internal interface ISocket
    {
        void Connect(IPAddress ipAddress, int port);
        int Send(string message);
        string Receive();
        void Shutdown();
        void Close();
        ISocket Accept();
        void Listen();
        void Bind(IPAddress ipAddress, int port);
    }
}