public sealed class BuildData
{
    public BuildData(
        ICakeContext context,
        string configuration,
        SolutionData solution)
    {
        this.Configuration = configuration;
        this.Solution = solution;

        this.DirectoriesToDelete = context
            .GetDirectories(this.Solution.ArtifactsDirectory)
            .Concat(context.GetDirectories("./**/bin"))
            .Concat(context.GetDirectories("./**/obj"))
            .OrderBy(directory => directory.ToString())
            .ToList();
    }

    public string Configuration { get; }
    public SolutionData Solution { get; }
    public IEnumerable<DirectoryPath> DirectoriesToDelete { get; }
}

public sealed class SolutionData
{
    public SolutionData(
        string artifactsDirectory,
        string slnPath,
        IReadOnlyCollection<string> projectsPath,
        IReadOnlyCollection<string> commands)
    {
        this.ArtifactsDirectory = artifactsDirectory;
        this.SlnPath = slnPath;
        this.ProjectsPath = projectsPath;
        this.Commands = commands;
    }

    public string ArtifactsDirectory { get; }
    public string SlnPath { get; }
    public IReadOnlyCollection<string> ProjectsPath { get; }
    public IReadOnlyCollection<string> Commands { get; }
}
