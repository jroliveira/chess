namespace Chess
{
    using System;
    using System.Linq;

    using Chess.Lib.Data.Commands;
    using Chess.Lib.Exceptions;
    using Chess.Lib.Extensions;
    using Chess.Lib.Monad;
    using Chess.Lib.Monad.Extensions;
    using Chess.Models;

    using static System.String;

    public sealed class Game : IGame
    {
        private readonly Entities.Chessboard chessboard;
        private readonly MountChessboardCommand mountChessboard;

        public Game()
            : this(new Entities.Chessboard(), new MountChessboardCommand())
        {
        }

        internal Game(Entities.Chessboard chessboard, MountChessboardCommand mountChessboard)
        {
            this.chessboard = chessboard;
            this.mountChessboard = mountChessboard;
        }

        public Try<Chessboard> Start()
        {
            this.mountChessboard.Execute(this.chessboard);
            return this.Map();
        }

        public Try<Chessboard> Move(Option<string> piecePositionOption, Option<string> newPositionOption)
        {
            var piecePosition = piecePositionOption.GetOrElse(Empty);
            if (IsNullOrEmpty(piecePosition))
            {
                return new ArgumentNullException(nameof(piecePosition), "Piece position cannot be null or empty.");
            }

            var newPosition = newPositionOption.GetOrElse(Empty);
            if (IsNullOrEmpty(newPosition))
            {
                return new ArgumentNullException(nameof(newPosition), "New position cannot be null or empty.");
            }

            var position = newPosition.ToPosition();
            var pieceOption = this.chessboard.GetPiece(piecePosition.ToPosition());
            var piece = pieceOption.GetOrElse(default);

            if (piece == null)
            {
                return new ChessException($"Piece '{piecePosition}' don't exist.");
            }

            this.chessboard.MovePiece(piece, position);
            return this.Map();
        }

        private Chessboard Map()
        {
            var pieces = new Piece[this.chessboard.Files.Count, this.chessboard.Ranks.Count];

            for (var fileIndex = 0; fileIndex < this.chessboard.Files.Count; fileIndex++)
            {
                for (var rankIndex = 0; rankIndex < this.chessboard.Ranks.Count; rankIndex++)
                {
                    var file = this.chessboard.Files.ElementAt(fileIndex);
                    var rank = this.chessboard.Ranks.ElementAt(rankIndex);
                    var pieceOption = this.chessboard.GetPiece(new Entities.Position(file, rank));
                    var piece = pieceOption.GetOrElse(default);

                    if (piece == null)
                    {
                        continue;
                    }

                    pieces[fileIndex, rankIndex] = new Piece(piece);
                }
            }

            return new Chessboard(
                this.chessboard.Files,
                this.chessboard.Ranks,
                pieces);
        }
    }
}
