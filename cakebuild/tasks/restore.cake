Task("Restore")
    .Does(() => DotNetCoreRestore(solutionPath));