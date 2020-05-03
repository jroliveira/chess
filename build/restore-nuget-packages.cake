Task("Restore-NuGet-Packages")
    .Does<BuildData>(data =>
    {
        using(var process = StartAndReturnProcess("dotnet", new ProcessSettings { Arguments = "paket install" }))
        {
            process.WaitForExit();
        }

        DotNetCoreRestore(data.Solution.SlnPath);
    });
