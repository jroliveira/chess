namespace Chess.Client.Infra.Win32
{
    using System;
    using System.Runtime.InteropServices;

#pragma warning disable IDE0009 // Member access should be qualified.
#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable IDE0021 // Use expression body for constructors
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0049 // Simplify Names
#pragma warning disable SA1101 // Prefix local calls with this
#pragma warning disable SA1121 // Use built-in type alias
#pragma warning disable SA1201 // Elements should appear in the correct order
#pragma warning disable SA1306 // Field names should begin with lower-case letter
#pragma warning disable SA1305 // Field names should not use Hungarian notation
#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1310 // Field names should not contain underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1401 // Fields should be private
    internal static class Win32Api
    {
        internal const int MF_BYCOMMAND = 0x00000000;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int SC_SIZE = 0xF000;
        internal const int STD_OUTPUT_HANDLE = -11;

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

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(
            int stdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetCurrentConsoleFontEx(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            [In, Out] PCONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetCurrentConsoleFontEx(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            [In, Out] PCONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal class PCONSOLE_FONT_INFOEX
        {
            private int cbSize;

            public PCONSOLE_FONT_INFOEX()
            {
                cbSize = Marshal.SizeOf(typeof(PCONSOLE_FONT_INFOEX));
            }

            public int FontIndex;
            public COORD FontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            private short X;
            private short Y;

            public COORD(short posX, short posY)
            {
                X = posX;
                Y = posY;
            }
        }
    }
}
