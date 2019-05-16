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

        internal static void SetConsoleFont(string fontName)
        {
            var info = new PCONSOLE_FONT_INFOEX
            {
                FaceName = fontName,
            };

            SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), false, info);
        }

        internal static void SetConsoleFontSize(short fontSize)
        {
            var size = new COORD(fontSize, fontSize);
            var info = new PCONSOLE_FONT_INFOEX();
            GetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), false, info);
            info.FontSize = size;
            SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), false, info);
        }
    }
}
