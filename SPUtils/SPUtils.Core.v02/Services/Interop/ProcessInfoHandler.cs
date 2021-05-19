using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPUtils.Core.v02.Services.Interop
{
    public class ProcessInfoHandler
    {
        public static PROCESS_MEMORY_COUNTERS_32 GetProcessMemoryUsageCounters()
        {
            #region TRY_BLOCK_REGION
#if DEBUG
                /*
                #endif
                try
                {
                #if DEBUG
                */
#endif
            #endregion

            PROCESS_MEMORY_COUNTERS_32 memCounters;
            memCounters.cb = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS_32));
            if (!PsAPI.GetProcessMemoryInfo(Kernel32.GetCurrentProcess(), out memCounters, memCounters.cb))
            {
                throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
                //mem_counters.cb = (uint)int.MaxValue;
                //return mem_counters;
            }
            else
                return memCounters;       

            #region CATCH_BLOCK_REGION
#if DEBUG
                /*
                #endif              
                }
                catch(Exception)
                {
                    throw;
                }  
                #if DEBUG
                */
#endif
            #endregion         
        }

        public static bool ReleaseUnusedMemoryPages()
        {
            #region TRY_BLOCK_REGION
#if DEBUG
            /*
                #endif
                try
                {
                #if DEBUG
                */
#endif
            #endregion

            return PsAPI.EmptyWorkingSet(System.Diagnostics.Process.GetCurrentProcess().Handle);

            #region CATCH_BLOCK_REGION
#if DEBUG
                /*
                #endif              
                }
                catch (Exception)
                {
                    throw;
                } 
                #if DEBUG
                */
#endif
            #endregion                 
        }

        public static bool SetProcessMemorySizeBoundries(uint MinimumSizeLimit, uint MaximumSizeLimit)
        {
            #region TRY_BLOCK_REGION
#if DEBUG
                /*
                #endif
                try
                {
                #if DEBUG
                */
#endif
            #endregion

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return Kernel32.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, MinimumSizeLimit, MaximumSizeLimit);
            else
                return false;

            #region CATCH_BLOCK_REGION
#if DEBUG
                /*
                #endif              
                }
                catch (Exception)
                {
                    throw;
                }
                #if DEBUG
                */
#endif
            #endregion         
        }

        public static bool FlushUnusedMemory(string Fname)
        {
            #region TRY_BLOCK_REGION
#if DEBUG
                /*
                #endif
                try
                {
                #if DEBUG
                */
#endif
            #endregion

            bool res = false;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                res = PsAPI.EmptyWorkingSet(System.Diagnostics.Process.GetCurrentProcess().Handle);
            else
                res = false;

            return res;
            #region CATCH_BLOCK_REGION
#if DEBUG
                /*
                #endif              
                }
                catch (Exception)
                {
                    throw;
                }
                #if DEBUG
                */
#endif
            #endregion         
        }

        public static void SetDllSourceDirectory(string absolutePath)
        {
            try
            {
                Kernel32.SetDllDirectory(absolutePath);
            }
            catch
            {
                throw;
            }
        }
    }
}
