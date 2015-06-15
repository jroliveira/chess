using Chess.Validations;

namespace Chess.Test.Fakes.Validations
{
    internal class FakeValidate : Validate
    {
        private readonly bool _isValid;

        public FakeValidate(bool isValid)
        {
            _isValid = isValid;
        }

        protected override bool IsValidRule(Position newPosition)
        {
            return _isValid;
        }
    }
}