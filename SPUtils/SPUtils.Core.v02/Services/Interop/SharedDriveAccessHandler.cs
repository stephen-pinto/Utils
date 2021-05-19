using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPUtils.Core.v02.Services.Interop
{
    /// <summary>
    /// Handling access and mount for remote drives or files
    /// </summary>
    public class SharedDriveAccessHandler
    {
        /// <summary>
        /// Mounts the specified remote unc.
        /// </summary>
        /// <param name="remoteUNC">The remote unc.</param>
        /// <param name="driveName">Name of the drive.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="promptUser">if set to <c>true</c> [prompt user].</param>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        public static void Mount(string remoteUNC, string driveName, string userName, string password, bool promptUser)
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

            NETRESOURCE networkResource = new NETRESOURCE();
            networkResource.dwType = Mpr32.RESOURCETYPE_DISK;
            networkResource.lpLocalName = driveName;
            networkResource.lpRemoteName = remoteUNC;
            networkResource.lpProvider = null;
            networkResource.dwScope = 2; //RES_SCOPE_GLOBALNET
            networkResource.dwUsage = 1; //RES_ISE_CONNECT

            uint result = 0;

            if (!promptUser)
            {
                //if user need not be prompted for a username and password
                result = Mpr32.WNetAddConnection2W(ref networkResource, password, userName, 0);
            }
            else
            {
                //if user must be prompted for a username and password
                uint flags = Mpr32.CONNECT_INTERACTIVE | Mpr32.CONNECT_PROMPT | Mpr32.CONNECT_CMD_SAVECRED | Mpr32.CONNECT_UPDATE_PROFILE;
                result = Mpr32.WNetAddConnection2W(ref networkResource, password, userName, flags);
            }

            //if already credentials exist then call with null credentails
            if ((int)result == 1219)
                result = Mpr32.WNetAddConnection2W(ref networkResource, null, null, 0);

            if (result != BaseAPI.NO_ERROR && result != 85)
            {
                throw new System.ComponentModel.Win32Exception((int)result);
            }

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

        /// <summary>
        /// Unmounts the specified drive.
        /// </summary>
        /// <param name="driveName">Name of the drive.</param>
        public static void Unmount(string driveName)
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

            uint result = Mpr32.WNetCancelConnection2(driveName, Mpr32.CONNECT_UPDATE_PROFILE, false);
            if (result != BaseAPI.NO_ERROR)
            {

            }

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
    }
}
