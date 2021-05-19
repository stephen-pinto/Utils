using System.Runtime.InteropServices;

namespace SPUtils.Core.v02.Utils.Local
{
    public static class LocalEnvironment
    {
        public static bool IsWindows
        {
            get
            {
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            }
        }

        public static string GetHostWindowsVersion()
        {
            var osver = System.Environment.OSVersion;
            if (osver.Version.Major == 5)
                return "WindowsXP";
            else if (osver.Version.Major == 6 && osver.Version.Minor == 0)
                return "WindowsVista";
            else if (osver.Version.Major == 6 && osver.Version.Minor == 1)
                return "Windows7";
            else if (osver.Version.Major == 6 && osver.Version.Minor == 2)
                return "Windows8";
            else if (osver.Version.Major == 6 && osver.Version.Minor > 3)
                return "Windows8.1";
            else if (osver.Version.Major == 10)
                return "Windows10";
            else
                return "Unknown";
        }

        public static string GetIP()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "127.0.0.1";
        }
    }
}
