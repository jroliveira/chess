namespace Chess.Game.Validations
{
    internal interface IValidator
    {
        bool Validate(Position newPosition);
    }
}