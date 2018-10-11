namespace Chess.Grains
{
    using Chess.Interfaces;
    using Orleans;

    public class Player : Grain, IPlayer, IGrainWithStringKey
    {
        public Player(string name) => this.Name = name;

        public string Name { get; }
    }
}
