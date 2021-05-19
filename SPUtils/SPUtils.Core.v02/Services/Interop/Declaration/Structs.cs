using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPUtils.Core.v02.Services.Interop
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NETRESOURCE
    {
        public int dwScope;
        public int dwType;
        public int dwDisplayType;
        public int dwUsage;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
        public string lpLocalName;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
        public string lpRemoteName;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
        public string lpComment;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
        public string lpProvider;
    }

    //32bit or 64bit depending on OS
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Size = 40)]
    public struct PROCESS_MEMORY_COUNTERS
    {
        public uint cb;                         // The size of the structure, in bytes (DWORD).
        public uint PageFaultCount;             // The number of page faults (DWORD).
        public uint PeakWorkingSetSize;         // The peak working set size, in bytes (SIZE_T).
        public uint WorkingSetSize;             // The current working set size, in bytes (SIZE_T).
        public uint QuotaPeakPagedPoolUsage;    // The peak paged pool usage, in bytes (SIZE_T).
        public uint QuotaPagedPoolUsage;        // The current paged pool usage, in bytes (SIZE_T).
        public uint QuotaPeakNonPagedPoolUsage; // The peak nonpaged pool usage, in bytes (SIZE_T).
        public uint QuotaNonPagedPoolUsage;     // The current nonpaged pool usage, in bytes (SIZE_T).
        public uint PagefileUsage;              // The Commit Charge value in bytes for this process (SIZE_T). Commit Charge is the total amount of memory that the memory manager has committed for a running process.
        public uint PeakPagefileUsage;          // The peak value in bytes of the Commit Charge during the lifetime of this process (SIZE_T).
    }

    //32bit only
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Size = 40)]
    public struct PROCESS_MEMORY_COUNTERS_32
    {
        public uint cb;
        public uint PageFaultCount;
        public int PeakWorkingSetSize;
        public int WorkingSetSize;
        public int QuotaPeakPagedPoolUsage;
        public int QuotaPagedPoolUsage;
        public int QuotaPeakNonPagedPoolUsage;
        public int QuotaNonPagedPoolUsage;
        public int PagefileUsage;
        public int PeakPagefileUsage;
    }
}
