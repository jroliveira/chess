Task("Delete-Temp-Directories")
    .ContinueOnError()
    .Does<BuildData>(data =>
    {
        DeleteDirectories(data.DirectoriesToDelete, new DeleteDirectorySettings
        {
            Recursive = true,
            Force = true,
        });

        data.DirectoriesToDelete
            .ToList()
            .ForEach(directory => Information($"  Delete completed for {directory}"));
    });
