namespace Chess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Chess.Lib.Data.Commands;
    using Chess.Lib.Exceptions;
    using Chess.Lib.Extensions;
    using Chess.Lib.Monad;
    using Chess.Lib.Monad.Extensions;
    using Chess.Models;

    public sealed class Game : IGame
    {
        private readonly Entities.Chessboard chessboard;
        private readonly MountChessboardCommand mountChessboard;
        private readonly IDictionary<string, Player> players;

        public Game()
            : this(new Entities.Chessboard(), new MountChessboardCommand(), new Dictionary<string, Player>())
        {
        }

        internal Game(
            Entities.Chessboard chessboard,
            MountChessboardCommand mountChessboard,
            IDictionary<string, Player> players)
        {
            this.chessboard = chessboard;
            this.mountChessboard = mountChessboard;
            this.players = players;

            this.mountChessboard.Execute(this.chessboard);
        }

        public Try<Chessboard> Start() => this.chessboard.ToModel();

        public Try<Player> JoinPlayer(Option<string> playerName)
        {
            if (!playerName.IsDefined)
            {
                return new ArgumentNullException(nameof(playerName), "Player name cannot be null or empty.");
            }

            var playerCount = this.players.Count;
            if (playerCount == 2)
            {
                return new ChessException("Too many players in the game.");
            }

            var player = new Player(playerName.Get(), playerCount == 0);
            this.players.Add(player, player);

            return player;
        }

        public Try<Chessboard> MovePiece(Option<string> piecePosition, Option<string> newPosition, Option<string> playerName)
        {
            if (!piecePosition.IsDefined)
            {
                return new ArgumentNullException(nameof(piecePosition), "Piece position cannot be null or empty.");
            }

            if (!newPosition.IsDefined)
            {
                return new ArgumentNullException(nameof(newPosition), "New position cannot be null or empty.");
            }

            if (!playerName.IsDefined)
            {
                return new ArgumentNullException(nameof(playerName), "Player name cannot be null or empty.");
            }

            if (!this.players.TryGetValue(playerName.Get(), out var player))
            {
                return new KeyNotFoundException($"Player name '{playerName.Get()}' is not playing.");
            }

            var piece = this.chessboard
                .GetPiece(piecePosition.Get().ToPosition())
                .GetOrElse(default);

            if (piece == null)
            {
                return new ChessException($"Piece '{piecePosition.Get()}' don't exist.");
            }

            if (piece.IsWhite != player.IsWhitePiece)
            {
                return new ChessException($"Piece '{piece}' does not belong to this player '{player}'.");
            }

            return this.chessboard
                .MovePiece(piece, newPosition.Get().ToPosition())
                .Match<Try<Chessboard>>(
                    _ => _,
                    _ => this.chessboard.ToModel());
        }
    }
}
