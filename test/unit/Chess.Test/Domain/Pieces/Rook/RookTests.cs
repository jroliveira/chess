namespace Chess.Test.Domain.Pieces.Rook
{
    using Chess.Test.Domain.Pieces.Shared;

    using Xunit;

    using static Chess.Domain.Pieces.Rook.Rook;
    using static Chess.PieceColor;

    public sealed class RookTests : PieceBaseTests
    {
        [Theory]
        [InlineData(WhitePiece, '♖')]
        [InlineData(BlackPiece, '♜')]
        public override void ImplicitOperatorChar_GivenPieceColor_ShouldReturn(PieceColor pieceColor, char expected) => this.ImplicitOperatorChar(CreateRook("a2", pieceColor), expected);

        [Theory]
        [InlineData(WhitePiece, '♖')]
        [InlineData(BlackPiece, '♜')]
        public override void ImplicitOperatorPiece_GivenPieceColor_ShouldReturn(PieceColor pieceColor, char symbol) => this.ImplicitOperatorPiece(CreateRook("a2", pieceColor), symbol);

        [Theory]
        [InlineData("a2", WhitePiece, '♖')]
        [InlineData("a7", BlackPiece, '♜')]
        public override void ToString_GivenPieceBase_ShouldReturn(string position, PieceColor pieceColor, char symbol) => this.ToString(CreateRook(position, pieceColor), position, symbol);
    }
}
