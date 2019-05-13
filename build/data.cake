public sealed class BuildData
{
    public BuildData(
        ICakeContext context,
        string configuration,
        bool isDebugging,
        Func<BuildData, Action<Exception>> errorHandler,
        SolutionData solution)
    {
        this.Configuration = configuration;
        this.IsDebugging = isDebugging;
        this.Solution = solution;

        this.DirectoriesToDelete = context
            .GetDirectories(this.Solution.ArtifactsDirectory)
            .Concat(context.GetDirectories("./**/bin"))
            .Concat(context.GetDirectories("./**/obj"))
            .OrderBy(directory => directory.ToString())
            .ToList();

        this.ErrorHandler = errorHandler(this);
    }

    public string Configuration { get; }
    public bool IsDebugging { get; }
    public Action<Exception> ErrorHandler { get; }
    public SolutionData Solution { get; }
    public IEnumerable<DirectoryPath> DirectoriesToDelete { get; }
}

public sealed class SolutionData
{
    public SolutionData(
        Func<string> getVersion,
        string artifactsDirectory,
        string slnPath,
        IReadOnlyCollection<string> projectsPath,
        IReadOnlyCollection<string> commands)
    {
        this.GetVersion = getVersion;
        this.ArtifactsDirectory = artifactsDirectory;
        this.SlnPath = slnPath;
        this.ProjectsPath = projectsPath;
        this.Commands = commands;
    }

    public Func<string> GetVersion { get; }
    public string ArtifactsDirectory { get; }
    public string SlnPath { get; }
    public IReadOnlyCollection<string> ProjectsPath { get; }
    public IReadOnlyCollection<string> Commands { get; }
}
