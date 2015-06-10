using System.Net;
using Chess.Exceptions;
using Chess.Game;
using Chess.Game.Extensions;
using Chess.Game.Multiplayer;
using Chess.Game.Multiplayer.EventHandlers;

namespace Chess
{
    public class ChessGame
    {
        private Multiplayer _player;

        private readonly Chessboard _chessboard;
        private readonly MountChessboard _mountChessboard;
        private readonly Server _server;
        private readonly Client _client;

        public char[] Files { get { return _chessboard.Files; } }
        public char[] Ranks { get { return _chessboard.Ranks; } }

        public ChessGame()
        {
            _chessboard = new Chessboard();
            _mountChessboard = new MountChessboard(_chessboard);

            _server = new Server();
            _client = new Client();
        }

        public event ErrorEventHandler Error;
        public event PlayedEventHandler Played;
        public event ConnectedEventHandler Connected;

        public void Start()
        {
            _mountChessboard.Mount();
        }

        public void Connect(IPAddress ipAddress, int port)
        {
            _player = _client;

            _client.Connected += OnConnected;
            _client.Played += OnPlayed;
            _client.Error += Error;

            _client.Connect(ipAddress, port);
        }

        public void WaitingForOpponent()
        {
            _player = _server;

            _server.Connected += OnConnected;
            _server.Played += OnPlayed;
            _server.Error += Error;

            _server.Listening();
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
            MovePiece(piecePosition, newPosition);

            var handler = Played;
            if (handler != null)
            {
                handler(piecePosition, newPosition);
            }
        }

        public void Move(string piecePosition, string newPosition)
        {
            MovePiece(piecePosition, newPosition);

            if (_player == null)
            {
                return;
            }

            _player.SendTheMove(piecePosition, newPosition);
            _player.WaitingTheMove();
        }

        private void MovePiece(string piecePosition, string newPosition)
        {
            var position = newPosition.ToPosition();
            var piece = _chessboard.GetPiece(piecePosition.ToPosition());

            if (piece == null)
            {
                throw new PieceIsNullException(piecePosition);
            }

            _chessboard.MovePiece(piece, position);
        }

        public ChessPiece GetPiece(char file, char rank)
        {
            var position = new Position(file, rank);
            var piece = _chessboard.GetPiece(position);

            return piece == null
                ? null
                : new ChessPiece(piece.Name, piece.Player);
        }
    }
}