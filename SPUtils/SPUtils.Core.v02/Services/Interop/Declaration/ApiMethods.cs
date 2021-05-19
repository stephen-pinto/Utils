using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPUtils.Core.v02.Services.Interop
{
    public class BaseAPI
    {
        public const uint NO_ERROR = 0;
    }

    /*
    ******************************************************************************************
    *  Following class contains methods belonging to advapi32.dll in Windows API
    ******************************************************************************************
    */

    public class AdvAPI32
    {
        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        public static extern uint LogonUser(string lpszUserName,
             string lpszDomain,
             string lpszPassword,
             int dwLogonType,
             int dwLogonProvider,
             ref IntPtr phToken);

        [System.Runtime.InteropServices.DllImport("advapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern uint DuplicateToken(IntPtr hToken,
              int impersonationLevel,
              ref IntPtr hNewToken);

        [System.Runtime.InteropServices.DllImport("advapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();
    }

    /*
    ******************************************************************************************
    * Following class contains methods belonging to kernel32.dll in Windows API
    ******************************************************************************************
    */

    public class Kernel32
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool CloseHandle(
            IntPtr handle);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(
            IntPtr proc, 
            uint min, 
            uint max);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetDllDirectory(string lpPathName);
    }

    /*
    ******************************************************************************************
    * Following class contains methods belonging to mpr.dll in Windows API
    ******************************************************************************************
    */

    public class Mpr32
    {
        #region Constants specific to this API

        public const int RESOURCE_CONNECTED = 0x00000001;
        public const int RESOURCE_GLOBALNET = 0x00000002;
        public const int RESOURCE_REMEMBERED = 0x00000003;

        public const int RESOURCETYPE_ANY = 0x00000000;
        public const int RESOURCETYPE_DISK = 0x00000001;
        public const int RESOURCETYPE_PRINT = 0x00000002;

        public const int RESOURCEDISPLAYTYPE_GENERIC = 0x00000000;
        public const int RESOURCEDISPLAYTYPE_DOMAIN = 0x00000001;
        public const int RESOURCEDISPLAYTYPE_SERVER = 0x00000002;
        public const int RESOURCEDISPLAYTYPE_SHARE = 0x00000003;
        public const int RESOURCEDISPLAYTYPE_FILE = 0x00000004;
        public const int RESOURCEDISPLAYTYPE_GROUP = 0x00000005;

        public const int RESOURCEUSAGE_CONNECTABLE = 0x00000001;
        public const int RESOURCEUSAGE_CONTAINER = 0x00000002;


        public const int CONNECT_INTERACTIVE = 0x00000008;
        public const int CONNECT_PROMPT = 0x00000010;
        public const int CONNECT_REDIRECT = 0x00000080;
        public const int CONNECT_UPDATE_PROFILE = 0x00000001;
        public const int CONNECT_COMMANDLINE = 0x00000800;
        public const int CONNECT_CMD_SAVECRED = 0x00001000;

        public const int CONNECT_LOCALDRIVE = 0x00000100;    
    
        #endregion

        [System.Runtime.InteropServices.DllImport("Mpr.dll")]
        public static extern uint WNetUseConnection(
            IntPtr hwndOwner,
            NETRESOURCE lpNetResource,
            string lpPassword,
            string lpUserID,
            int    dwFlags,
            string lpAccessName,
            string lpBufferSize,
            string lpResult);

        [System.Runtime.InteropServices.DllImport("Mpr.dll")]
        public static extern uint WNetAddConnection2W(
            ref NETRESOURCE lpNetResource,
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] 
            string lpPassword,
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] 
            string lpUsername,
            uint dwFlags);

        [System.Runtime.InteropServices.DllImport("Mpr.dll")]
        public static extern uint WNetAddConnection3(
            IntPtr hWndOwner, 
            ref NETRESOURCE lpNetResource, 
            string lpPassword, 
            string lpUserName,
            uint dwFlags);

        [System.Runtime.InteropServices.DllImport("Mpr.dll")]
        public static extern uint WNetCancelConnection2(
            string lpName,
            int dwFlags,
            bool fForce);
    }

    /*
    ******************************************************************************************
    * Following class contains methods belonging to psapi32.dll in Windows API
    ******************************************************************************************
    */

    public class PsAPI
    {
        [System.Runtime.InteropServices.DllImport("psapi.dll", SetLastError = true)]
        public static extern bool GetProcessMemoryInfo(
            IntPtr hProcess, 
            out PROCESS_MEMORY_COUNTERS_32 counters, 
            uint size);

        [System.Runtime.InteropServices.DllImport("psapi.dll")]
        public static extern bool EmptyWorkingSet(
            IntPtr hProcess);
    }

}
