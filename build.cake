#addin "Cake.Figlet&version=1.2.0"

#load "build/*.cake"

Setup<BuildData>(context =>
{
    Information(Figlet("Chess"));

    return new BuildData(
        context,
        GetConfiguration(),
        IsDebugging(),
        ErrorHandler,
        new SolutionData(
            GetVersion,
            "./artifacts",
            "./Chess.sln",
            new[]
            {
                "./src/orleans/ui/Chess.Client",
                "./src/orleans/ui/Chess.SiloHost",
            },
            new[]
            {
                "dotnet artifacts/Chess.Client.dll",
                "dotnet artifacts/Chess.Client.dll",
                "dotnet artifacts/Chess.SiloHost.dll",
            }));
});

Task("Default")
    .IsDependentOn("Delete-Temp-Directories")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-Solution")
    .IsDependentOn("Run-Tests");

Task("Start-Apps")
    .IsDependentOn("Delete-Temp-Directories")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-Solution")
    .IsDependentOn("Publish-Console-Apps")
    .IsDependentOn("Run-Cmds");

RunTarget(Argument("target", "Default"));
