namespace Chess.Test.Fakes.Validations
{
    using Chess.Validations;

    internal class FakeValidate : Validate
    {
        private readonly bool isValid;

        public FakeValidate(bool isValid)
        {
            this.isValid = isValid;
        }

        protected override bool IsValidRule(Position newPosition)
        {
            return this.isValid;
        }
    }
}