namespace Chess.Validations
{
    internal interface IValidator
    {
        bool Validate(Position newPosition);
    }
}