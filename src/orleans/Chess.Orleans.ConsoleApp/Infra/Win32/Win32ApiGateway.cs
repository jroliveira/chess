namespace Chess.Orleans.ConsoleApp.Infra.Win32
{
    using Chess.Infra.Monad;

    using static System.IntPtr;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.Infra.Win32.Win32Api;

    internal static class Win32ApiGateway
    {
        internal static Unit DisableWindowResize()
        {
            var handle = GetConsoleWindow();
            if (handle == Zero)
            {
                return Unit();
            }

            var sysMenu = GetSystemMenu(handle, false);

            DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);

            return Unit();
        }
    }
}
