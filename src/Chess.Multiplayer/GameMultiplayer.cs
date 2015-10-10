using System.Net;
using Chess.Multiplayer.EventHandlers;

namespace Chess.Multiplayer
{
    public class GameMultiplayer : Game, IGameMultiplayer
    {
        private Multiplayer _player;

        private readonly Server _server;
        private readonly Client _client;

        public GameMultiplayer()
        {
            _server = new Server();
            _client = new Client();
        }

        public event ErrorEventHandler Error;
        public event PlayedEventHandler Played;
        public event ConnectedEventHandler Connected;

        public override void Move(string piecePosition, string newPosition)
        {
            base.Move(piecePosition, newPosition);

            _player.SendTheMove(piecePosition, newPosition);
            _player.WaitingTheMove();
        }

        public void WaitingTheMove()
        {
            _player.WaitingTheMove();
        }

        public void WaitingForOpponent()
        {
            _player = _server;

            _server.Connected += OnConnected;
            _server.Played += OnPlayed;
            _server.Error += Error;

            _server.Listen();
        }

        public void Connect(string ipAddress, string port)
        {
            _player = _client;

            _client.Connected += OnConnected;
            _client.Played += OnPlayed;
            _client.Error += Error;

            _client.Connect(IPAddress.Parse(ipAddress), int.Parse(port));
        }

        private void OnConnected()
        {
            _player.Played += OnPlayed;

            var handler = Connected;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnPlayed(string piecePosition, string newPosition)
        {
            base.Move(piecePosition, newPosition);

            var handler = Played;
            if (handler != null)
            {
                handler(piecePosition, newPosition);
            }
        }
    }
}