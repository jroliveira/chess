namespace Chess.Domain.Pieces.Shared
{
    using System;
    using System.Collections.Generic;

    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Bishop;
    using Chess.Domain.Pieces.King;
    using Chess.Domain.Pieces.Knight;
    using Chess.Domain.Pieces.Pawn;
    using Chess.Domain.Pieces.Queen;
    using Chess.Domain.Pieces.Rook;
    using Chess.Domain.Shared;
    using Chess.Domain.User;
    using Chess.Infra.Monad;

    using static Chess.Constants.ErrorMessages.Piece;
    using static Chess.Domain.Pieces.Bishop.Bishop;
    using static Chess.Domain.Pieces.King.King;
    using static Chess.Domain.Pieces.Knight.Knight;
    using static Chess.Domain.Pieces.Pawn.Pawn;
    using static Chess.Domain.Pieces.Queen.Queen;
    using static Chess.Domain.Pieces.Rook.Rook;
    using static Chess.PieceColor;

    internal abstract class PieceBase : IEquatable<PieceBase>
    {
        private readonly Position position;
        private readonly IReadOnlyCollection<Position> logMoves;
        private readonly (char White, char Black) symbol;
        private readonly ValidatorBase validator;

        protected PieceBase(
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves,
            (char, char) symbol,
            ValidatorBase validator)
        {
            this.position = position;
            this.Color = color;
            this.logMoves = new List<Position>(logMoves ?? new List<Position>()) { position };
            this.symbol = symbol;
            this.validator = validator;
        }

        internal bool IsFirstMove => this.logMoves.Count == 1;

        internal PieceColor Color { get; }

        public static implicit operator string(PieceBase piece) => piece.ToString();

        public static implicit operator char(PieceBase piece) => piece.ToChar();

        public static implicit operator Piece(PieceBase piece) => piece.ToChar();

        public override string ToString() => $"{this.ToChar()}{this.position}";

        public char ToChar() => this.Color switch
        {
            WhitePiece => this.symbol.White,
            BlackPiece => this.symbol.Black,
            _ => throw DoesNotExist(this.Color),
        };

        public bool Equals(PieceBase? other) => this.Equals(other?.position);

        public bool Equals(Position? other) => this.position.Equals(other);

        internal static Try<PieceBase> CreatePiece(
            Type type,
            Position position,
            PieceColor pieceColor) => CreatePiece(
                type,
                position,
                pieceColor,
                new List<Position>());

        internal static Try<PieceBase> CreatePiece(
            Type type,
            Position position,
            PieceColor color,
            IReadOnlyCollection<Position> logMoves) => type.Name switch
            {
                nameof(Bishop) => CreateBishop(position, color, logMoves),
                nameof(King) => CreateKing(position, color, logMoves),
                nameof(Knight) => CreateKnight(position, color, logMoves),
                nameof(Pawn) => CreatePawn(position, color, logMoves),
                nameof(Queen) => CreateQueen(position, color, logMoves),
                nameof(Rook) => CreateRook(position, color, logMoves),
                _ => DoesNotExist(type)
            };

        internal Try<PieceBase> Move(Position newPosition) => CreatePiece(
            this.GetType(),
            newPosition,
            this.Color,
            this.logMoves);

        internal bool CanMove(Position newPosition, Chessboard chessboard) => this.validator.Validate(this, newPosition, chessboard);

        internal bool BelongsTo(Player player) => this.Color.Equals(player.PlayingWith);

        internal bool SameColor(PieceBase other) => this.Color.Equals(other.Color);

        internal (int FilesToMove, int RanksToMove) GetDistanceTo(Position newPosition) => this.position.GetDistanceTo(newPosition);

        internal (Direction Horizontal, Direction Vertical) GetDirectionTo(Position newPosition) => this.position.GetDirectionTo(newPosition);
    }
}
