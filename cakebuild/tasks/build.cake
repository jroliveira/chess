Task("Build")
    .Does(() => DotNetCoreBuild(
        solutionPath,
        new DotNetCoreBuildSettings()
        {
            Configuration = configuration,
            OutputDirectory = outputDirectory,
            ArgumentCustomization = args => args.Append($"--no-restore"),
        }));