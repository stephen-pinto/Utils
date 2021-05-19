//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace SPUtils.Core.v02.Services.Interop
//{
//    /// <summary>
//    /// Handles basic user access and impersonation for temporary access.
//    /// </summary>
//    public class UserAccessHandler
//    {
//        private System.Security.Principal.WindowsImpersonationContext _prevImpersonationContext = null;

//        public void ImpersonateUser(string domainName, string userName, string userPasswd)
//        {
//            #region TRY_BLOCK_REGION
//#if DEBUG
//            /*
//                #endif
//                try
//                {
//                #if DEBUG
//                */
//#endif
//            #endregion
         
//            Impersonate(domainName, userName, userPasswd, LogonType.LOGON32_LOGON_NETWORK, LogonProvider.LOGON32_PROVIDER_DEFAULT);

//            #region CATCH_BLOCK_REGION
//#if DEBUG
//                /*
//                #endif              
//                }
//                catch(Exception)
//                {
//                    throw;
//                }  
//                #if DEBUG
//                */
//#endif
//            #endregion         
//        }

//        public void Impersonate(string domainName, string userName, string userPasswd, LogonType logonType, LogonProvider logonProvider)
//        {
//            IntPtr logonToken = IntPtr.Zero;
//            IntPtr logonTokenDuplicate = IntPtr.Zero;

//           #region TRY_BLOCK_REGION
//#if DEBUG
//            /*
//#endif
//            try
//            {
//#if DEBUG
//            */
//#endif
//            #endregion

//           if (AdvAPI32.LogonUser(userName, domainName, userPasswd, (int)logonType, (int)logonProvider, ref logonToken) != 0)
//           {
//               if (AdvAPI32.DuplicateToken(logonToken, (int)ImpersonationLevel.SecurityImpersonation, ref logonTokenDuplicate) != 0)
//               {
//                   var win_id = new System.Security.Principal.WindowsIdentity(logonTokenDuplicate);
//                   win_id.Impersonate();
//               }
//               else
//                   throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
//           }
//           else
//               throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());

//           #region CATCH_BLOCK_REGION
//#if DEBUG
//            /*
//#endif

//            }
//            catch (Exception)
//            {
//                throw;
//            }
//            finally
//            {
//                if (logonToken != IntPtr.Zero)
//                    Kernel32.CloseHandle(logonToken);

//                if (logonTokenDuplicate != IntPtr.Zero)
//                    Kernel32.CloseHandle(logonTokenDuplicate);
//            }

//#if DEBUG
//            */
//#endif
//            #endregion
//        }

//        public void UndoImpersonation()
//        {
//            if (this._prevImpersonationContext != null)
//                this._prevImpersonationContext.Undo();
//            this._prevImpersonationContext = null;
//        }
//    }
//}
