using System.Diagnostics;

using static System.Diagnostics.Process;
using static System.Diagnostics.ProcessWindowStyle;

Task("Run-Cmds")
    .DoesForEach<BuildData, string>(
        data => data.Solution.Commands,
        (data, command, _) => Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            WindowStyle = Normal,
            CreateNoWindow = true,
            UseShellExecute = true,
            Arguments = $"/C {command}",
        }));
