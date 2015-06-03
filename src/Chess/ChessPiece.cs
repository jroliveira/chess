namespace Chess
{
    public class ChessPiece
    {
        public string Name { get; private set; }
        public int Player { get; private set; }

        public ChessPiece(string name, int player)
        {
            Name = name;
            Player = player;
        }
    }
}
