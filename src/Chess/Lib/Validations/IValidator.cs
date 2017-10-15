namespace Chess.Lib.Validations
{
    using Chess.Entities;

    internal interface IValidator
    {
        bool Validate(Position newPosition);
    }
}