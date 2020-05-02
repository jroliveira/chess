namespace FluentAssertions
{
    internal static class AssertionExtensions
    {
        public static void ShouldBeEquivalentTo<T>(this T actual, object expected) => actual.Should().BeEquivalentTo(
            expected,
            config =>
            {
                config.AllowingInfiniteRecursion();
                config.IncludingAllDeclaredProperties();
                config.IncludingAllRuntimeProperties();

                return config;
            });
    }
}
