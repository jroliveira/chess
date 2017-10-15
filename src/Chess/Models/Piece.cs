namespace Chess.Models
{
    public sealed class Piece
    {
        public Piece(string name, Owner owner)
        {
            this.Name = name;
            this.Owner = owner;
        }

        public string Name { get; }

        public Owner Owner { get; }
    }
}
