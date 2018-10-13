readonly Func<string> GetConfiguration = () => Argument("configuration", "Release");

readonly Func<string> GetVersion = () => Argument<string>("version-suffix");

readonly Func<bool> IsDebugging = () => HasArgument("debugging");

readonly Func<BuildData, Action<Exception>> ErrorHandler = data => exception =>
{
    if (!data.IsDebugging)
    {
        throw exception;
    }

    Warning(exception);
};
