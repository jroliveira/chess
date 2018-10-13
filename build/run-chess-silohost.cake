using System.Diagnostics;

using static System.Diagnostics.Process;
using static System.Diagnostics.ProcessWindowStyle;

Task("Run-Chess-SiloHost")
    .Does(() => Start(new ProcessStartInfo
    {
        FileName = "cmd.exe",
        WindowStyle = Normal,
        CreateNoWindow = true,
        UseShellExecute = true,
        Arguments = @"/C dotnet src\orleans\ui\Chess.SiloHost\bin\Release\netcoreapp2.1\Chess.SiloHost.dll",
    }));
