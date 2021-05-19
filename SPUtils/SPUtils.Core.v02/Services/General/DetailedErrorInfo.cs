using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SPUtils.Core.v02.Services.General
{
    public class DetailedErrorInfo
    {
        public static string GetDetailedErrorReport(Exception excp, string title = "Exception report")
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

            string fileName = "<Unknown file>", funcName = "<Unknown function>";
            int lineNum = 0;

            string tagValFormat = Environment.NewLine + "{0, -15} : {1}";
            string finalStr = title + " - ";

            var stackTraceInfo = new System.Diagnostics.StackTrace(excp, true);

            //Get information from call stack
            for (int i = 0; i < stackTraceInfo.FrameCount; i++)
            {
                StackFrame frame = stackTraceInfo.GetFrame(i);

                if (frame == null)
                    break;

                var tempFname = frame.GetFileName();

                //Move till you can find a valid file name
                if (tempFname == null)
                    continue;

                var funcInfo = frame.GetMethod();

                //Get function name
                if (funcInfo != null && !string.IsNullOrEmpty(funcInfo.Name))
                    funcName = funcInfo.Name;

                //Get file name
                if (!string.IsNullOrEmpty(tempFname))
                    fileName = System.IO.Path.GetFileName(tempFname);

                //Get line number
                lineNum = frame.GetFileLineNumber();

                //Done here
                break;
            }

            finalStr += string.Format(tagValFormat, "Type", excp.GetType().Name);
            finalStr += string.Format(tagValFormat, "Message", excp.Message);
            finalStr += string.Format(tagValFormat, "Line no.", lineNum);
            finalStr += string.Format(tagValFormat, "Method", funcName);
            finalStr += string.Format(tagValFormat, "File", fileName);
            finalStr += string.Format(tagValFormat, "Stack trace", Environment.NewLine + GetStackTree(stackTraceInfo));

            if (excp.InnerException != null)
            {
                string innerExceptionReport = GetDetailedErrorReport(excp.InnerException, "Inner exception report");

                if (innerExceptionReport != null)
                {
                    finalStr += Environment.NewLine + "--------------------------------------------------" + Environment.NewLine + innerExceptionReport;
                }
            }

            return finalStr;
            #region CATCH_BLOCK_REGION
#if DEBUG
            /*
            #endif
            }
            catch(Exception)
            {
                return null;
            }
            #if DEBUG
            */
#endif
            #endregion        
        }

        public static string GetStackTree(StackTrace stack)
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

            if (stack == null)
                return null;

            string format = "at {0}.{1}() in {2} [line {3}]";
            string treeStr = "";

            for (int i = 0; i < stack.FrameCount; i++)
            {
                StackFrame frame = stack.GetFrame(i);

                if (frame == null)
                    break;

                var tempFname = frame.GetFileName();

                //Move till you can find a valid file name
                if (tempFname == null)
                    continue;

                var funcInfo = frame.GetMethod();

                if (funcInfo != null)
                {
                    string className = "<Unknown class>";
                    string funcName = "<Unknown method>";
                    string fileName = "<Unknown file>";

                    //Get class name
                    if (funcInfo.DeclaringType != null && !string.IsNullOrEmpty(funcInfo.DeclaringType.FullName))
                        className = funcInfo.DeclaringType.FullName;

                    //Get function name
                    if (!string.IsNullOrEmpty(funcInfo.Name))
                        funcName = funcInfo.Name;

                    //Get file name
                    if (!string.IsNullOrEmpty(tempFname))
                        fileName = System.IO.Path.GetFileName(tempFname);

                    //Get line number
                    int lineNum = frame.GetFileLineNumber();

                    //Prepare the message string
                    if (treeStr != "")
                        treeStr += Environment.NewLine;

                    treeStr += string.Format(format, className, funcName, fileName, lineNum);
                }
            }

            return treeStr;

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

    public class ActiveStack
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string FunctionName()
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

            //Get stack trace info until now
            StackTrace st = new StackTrace();

            //Since we want method name for the caller we will get 1st frame and not 0th
            StackFrame sf = st.GetFrame(1);

            //Return method name
            return sf.GetMethod().Name;

            #region CATCH_BLOCK_REGION
#if DEBUG
            /*
#endif
            }
            catch (Exception)
            {
                //throw;
                return "";
            }
#if DEBUG
            */
#endif
            #endregion
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string FunctionDescr()
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

            //Get stack trace info until now
            StackTrace st = new StackTrace();

            //Since we want method name for the caller we will get 1st frame and not 0th
            StackFrame sf = st.GetFrame(1);

            //Return full method name
            return sf.GetType().FullName + "::" + sf.GetMethod().Name + "()";

            #region CATCH_BLOCK_REGION
#if DEBUG
            /*
            #endif
            }
            catch(Exception)
            {
                //throw;
                return "";
            }
            #if DEBUG
            */
#endif
            #endregion        
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string FunctionDescr(int stackCallLevel)
        {
            //Get stack trace info until now
            StackTrace st = new StackTrace();

            //Since we want method name for the caller we will get 1st frame and not 0th
            StackFrame sf = st.GetFrame(stackCallLevel + 1);

            //Return full method name
            return sf.GetType().FullName + "::" + sf.GetMethod().Name + "()";
        }
    }
}
