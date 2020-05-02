namespace Chess.Orleans.ConsoleApp.Infra.Win32
{
    using System;
    using System.Runtime.InteropServices;

#pragma warning disable SA1305 // Field names should not use Hungarian notation
#pragma warning disable SA1310 // Field names should not contain underscore
    internal static class Win32Api
    {
        internal const int MF_BYCOMMAND = 0x00000000;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        internal static extern int DeleteMenu(
            IntPtr hMenu,
            int nPosition,
            int wFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetSystemMenu(
            IntPtr hWnd,
            bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetConsoleWindow();
    }
}
