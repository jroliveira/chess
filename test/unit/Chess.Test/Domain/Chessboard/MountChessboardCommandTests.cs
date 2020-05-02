namespace Chess.Test.Domain.Chessboard
{
    using System.Collections.Generic;

    using Chess;
    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;

    using FluentAssertions;

    using Xunit;

    using static Chess.Domain.Pieces.Bishop.Bishop;
    using static Chess.Domain.Pieces.King.King;
    using static Chess.Domain.Pieces.Knight.Knight;
    using static Chess.Domain.Pieces.Pawn.Pawn;
    using static Chess.Domain.Pieces.Queen.Queen;
    using static Chess.Domain.Pieces.Rook.Rook;
    using static Chess.PieceColor;

    public sealed class MountChessboardCommandTests
    {
        private static readonly Chessboard InitialChessboard = new List<PieceBase>(new List<PieceBase>
        {
            CreateRook("a8", BlackPiece),
            CreateKnight("b8", BlackPiece),
            CreateBishop("c8", BlackPiece),
            CreateQueen("d8", BlackPiece),
            CreateKing("e8", BlackPiece),
            CreateBishop("f8", BlackPiece),
            CreateKnight("g8", BlackPiece),
            CreateRook("h8", BlackPiece),
            CreatePawn("a7", BlackPiece),
            CreatePawn("b7", BlackPiece),
            CreatePawn("c7", BlackPiece),
            CreatePawn("d7", BlackPiece),
            CreatePawn("e7", BlackPiece),
            CreatePawn("f7", BlackPiece),
            CreatePawn("g7", BlackPiece),
            CreatePawn("h7", BlackPiece),
            CreatePawn("a2", WhitePiece),
            CreatePawn("b2", WhitePiece),
            CreatePawn("c2", WhitePiece),
            CreatePawn("d2", WhitePiece),
            CreatePawn("e2", WhitePiece),
            CreatePawn("f2", WhitePiece),
            CreatePawn("g2", WhitePiece),
            CreatePawn("h2", WhitePiece),
            CreateRook("a1", WhitePiece),
            CreateKnight("b1", WhitePiece),
            CreateBishop("c1", WhitePiece),
            CreateQueen("d1", WhitePiece),
            CreateKing("e1", WhitePiece),
            CreateBishop("f1", WhitePiece),
            CreateKnight("g1", WhitePiece),
            CreateRook("h1", WhitePiece),
        });

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │*R*│*N*│*B*│*Q*│*K*│*B*│*N*│*R*│ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │*p*│*p*│*p*│*p*│*p*│*p*│*p*│*p*│ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │   │   │   │   │   │   │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │   │   │   │   │   │   │   │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │   │   │   │   │   │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │   │   │   │   │   │   │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │ p │ p │ p │ p │ p │ p │ p │ p │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │ R │ N │ B │ Q │ K │ B │ N │ R │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Fact]
        public void MountChessboard_ShouldReturn()
        {
            Piece[,] expected = (Pieces)InitialChessboard;
            Piece[,] actual = (Pieces)MountChessboardCommand.MountChessboard();

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
