namespace Chess
{
    using System.Collections.Generic;

    using Chess.Entities;
    using Chess.Lib;
    using Chess.Lib.Data.Commands;
    using Chess.Lib.Exceptions;
    using Chess.Lib.Extensions;
    using Chess.Models;

    public class Game : Observable<IGame>, IGame
    {
        private readonly Chessboard chessboard;
        private readonly MountChessboardCommand mountChessboard;

        public Game()
            : this(new Chessboard(), new MountChessboardCommand())
        {
        }

        internal Game(Chessboard chessboard, MountChessboardCommand mountChessboard)
        {
            this.chessboard = chessboard;
            Observer<Chessboard>
                .Observe(this.chessboard)
                .Updated += _ => this.OnUpdate(this);

            this.mountChessboard = mountChessboard;
        }

        public IReadOnlyCollection<char> Files => this.chessboard.Files;

        public IReadOnlyCollection<char> Ranks => this.chessboard.Ranks;

        public void Start()
        {
            this.mountChessboard.Execute(this.chessboard);
        }

        public virtual void Move(string piecePosition, string newPosition)
        {
            var position = newPosition.ToPosition();
            var piece = this.chessboard.GetPiece(piecePosition.ToPosition());

            if (piece == null)
            {
                throw new ChessException($"Piece '{piecePosition}' don't exist.");
            }

            this.chessboard.MovePiece(piece, position);
        }

        public Piece GetPiece(char file, char rank)
        {
            var position = new Position(file, rank);
            var piece = this.chessboard.GetPiece(position);

            return piece == null
                ? null
                : new Piece(piece.Name, piece.Owner);
        }
    }
}