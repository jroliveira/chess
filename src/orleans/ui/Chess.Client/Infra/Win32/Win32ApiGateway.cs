namespace Chess.Client.Infra.Win32
{
    using static System.IntPtr;

    using static Chess.Client.Infra.Win32.Win32Api;

    internal static class Win32Gateway
    {
        internal static void DisableWindowResize()
        {
            var handle = GetConsoleWindow();
            var sysMenu = GetSystemMenu(handle, false);

            if (handle != Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
        }
    }
}
