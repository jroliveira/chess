#addin "Cake.Figlet&version=1.3.1"

#load "build/*.cake"

Setup<BuildData>(context =>
{
    Information(Figlet("Chess"));

    return new BuildData(
        context,
        GetConfiguration(),
        new SolutionData(
            "./artifacts",
            "./Chess.sln",
            new[]
            {
                "./src/orleans/Chess.Orleans.ConsoleApp",
                "./src/orleans/Chess.Orleans.SiloHost",
            },
            new[]
            {
                "dotnet artifacts/Chess.Orleans.SiloHost.dll",
                "dotnet artifacts/Chess.Orleans.ConsoleApp.dll",
                "dotnet artifacts/Chess.Orleans.ConsoleApp.dll",
                "dotnet artifacts/Chess.Orleans.ConsoleApp.dll",
                "dotnet artifacts/Chess.Orleans.ConsoleApp.dll",
                "dotnet artifacts/Chess.Orleans.ConsoleApp.dll",
            }));
});

Task("Default")
    .IsDependentOn("Delete-Temp-Directories")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-Solution")
    .IsDependentOn("Run-Tests");

Task("Start")
    .IsDependentOn("Delete-Temp-Directories")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-Solution")
    .IsDependentOn("Run-Tests")
    .IsDependentOn("Publish-Console-Apps")
    .IsDependentOn("Run-Cmds");

Task("Start-Apps")
    .IsDependentOn("Run-Cmds");

RunTarget(Argument("target", "Default"));
