namespace Chess.Models
{
    public class Piece
    {
        public Piece(string name, int player)
        {
            this.Name = name;
            this.Player = player;
        }

        public string Name { get; }

        public int Player { get; }
    }
}
