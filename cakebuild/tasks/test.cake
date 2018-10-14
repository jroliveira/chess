Task("Test")
    .DoesForEach(GetFiles($"./../test/**/*Test.csproj"), project => DotNetCoreTest(
        project.ToString(),
        new DotNetCoreTestSettings()
        {
            Configuration = configuration,
            NoBuild = true,
            ArgumentCustomization = args => args.Append($"--no-restore"),
        }));
