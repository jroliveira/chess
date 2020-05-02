namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    internal sealed class SelectOption
    {
        private SelectOption(int key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        internal int Key { get; }

        internal string Value { get; }

        public static implicit operator SelectOption((int Key, string Value) option) => new SelectOption(option.Key, option.Value);

        internal void Deconstruct(out int key, out string value) => (key, value) = (this.Key, this.Value);
    }
}
