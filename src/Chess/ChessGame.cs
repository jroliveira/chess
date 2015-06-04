using Chess.Exceptions;
using Chess.Game;
using Chess.Game.Extensions;
using Chess.Game.Multiplayer;
using Chess.Game.Multiplayer.EventHandlers;

namespace Chess
{
    public class ChessGame
    {
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

        public void Start()
        {
            _mountChessboard.Mount();
        }

        public void WaitingForOpponent()
        {
            _server.DataReceived += DataReceived;
            _server.Error += Error;
            _server.Waiting += Waiting;

            _server.Listening();
        }

        public void Connect()
        {
            _client.Connected += Connected;

            _client.Connect();
        }

        public void Move(string piecePosition, string newPosition)
        {
            var piece = _chessboard.GetPiece(piecePosition.ToPosition());

            if (piece == null)
            {
                throw new PieceIsNullException(piecePosition);
            }

            _chessboard.MovePiece(piece, newPosition.ToPosition());
        }

        public ChessPiece GetPiece(char file, char rank)
        {
            var position = new Position(file, rank);
            var piece = _chessboard.GetPiece(position);

            return piece == null
                ? null
                : new ChessPiece(piece.Name, piece.Player);
        }

        public event ErrorEventHandler Error;
        public event WaitingEventHandler Waiting;
        public event DataReceivedEventHandler DataReceived;
        public event ConnectedEventHandler Connected;
    }
}