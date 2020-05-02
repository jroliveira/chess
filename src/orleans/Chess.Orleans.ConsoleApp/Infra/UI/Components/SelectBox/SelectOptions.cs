namespace Chess.Orleans.ConsoleApp.Infra.UI
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using static System.String;

    internal sealed class SelectOptions : ReadOnlyDictionary<int, string>
    {
        private SelectOptions(IList<string> options)
            : this(options
                .Select(item => (options.IndexOf(item) + 1, item))
                .ToDictionary(item => item.Item1, item => item.Item2))
        {
        }

        private SelectOptions(IEnumerable<(int, string)> options)
            : this(options.ToDictionary(item => item.Item1, item => item.Item2))
        {
        }

        private SelectOptions(IDictionary<int, string> options)
            : base(CreateSelectOptions(options))
        {
        }

        public static implicit operator SelectOptions(List<(int, string)> options) => new SelectOptions(options);

        public static implicit operator SelectOptions(List<string> options) => new SelectOptions(options);

        private static IDictionary<int, string> CreateSelectOptions(IDictionary<int, string> options)
        {
            var menuOptions = new Dictionary<int, string> { { -1, Empty } };

            if (options.Any())
            {
                foreach (var (key, value) in options)
                {
                    menuOptions.Add(key, value);
                }

                menuOptions.Add(-2, Empty);
            }

            menuOptions.Add(0, "Back");
            menuOptions.Add(-3, Empty);

            return menuOptions;
        }
    }
}
