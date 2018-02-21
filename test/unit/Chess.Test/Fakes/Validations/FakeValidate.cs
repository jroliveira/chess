namespace Chess.Test.Fakes.Validations
{
    using Chess.Entities;
    using Chess.Lib.Validations;

    internal class FakeValidate : Validate
    {
        private readonly bool isValid;

        public FakeValidate(bool isValid) => this.isValid = isValid;

        protected override bool IsValidRule(Position newPosition) => this.isValid;
    }
}