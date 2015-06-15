namespace Chess.Models
{
    public class Piece
    {
        public string Name { get; private set; }
        public int Player { get; private set; }

        public Piece(string name, int player)
        {
            Name = name;
            Player = player;
        }
    }
}
