using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SPUtils.Core.v02.Services.General
{
    public class CmdLineArgsParser
    {
        private string _switchIdentifier;
        private char _assignmentIdentifier;

        public const string SOURCE_SWITCH = "[__src__]";

        /// <summary>
        /// Initializes a new instance of the <see cref="CmdLineArgsParser"/> class.
        /// </summary>
        /// <param name="switchIdntf">Switch identifier.</param>
        /// <param name="assignIdntf">Assignment identifier.</param>
        public CmdLineArgsParser(string switchIdntf = "/", char assignIdntf = '=')
        {
            _switchIdentifier = switchIdntf;
            _assignmentIdentifier = assignIdntf;
        }

        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public Dictionary<string, string> Parse(string[] args)
        {
            string awaitingSwitch = null;
            Dictionary<string, string> result = null;
            result = new Dictionary<string, string>();
            string source = null;

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

            //Iterate over all the arguments
            for (int i = 0; i < args.Length; i++)
            {
                string argument = args[i];

                //Check if its a switch
                if (argument.StartsWith(_switchIdentifier))
                {
                    //If we reach here it means no value is defined for an awaiting switch (if present)
                    if (awaitingSwitch != null)
                        awaitingSwitch = null;

                    //Trim down the switch to remove the switch identifier
                    string tempTrimmedArg = argument.Remove(0, _switchIdentifier.Length);

                    //If switch is defined with value using assigment identifier
                    if (tempTrimmedArg.Contains(_assignmentIdentifier))
                    {
                        int vpos = tempTrimmedArg.IndexOf(_assignmentIdentifier);

                        string[] swtchVal = new string[]{
                            tempTrimmedArg.Substring(0, vpos),
                            tempTrimmedArg.Substring(vpos + 1)
                        };

                        if (swtchVal.Length > 1 && !string.IsNullOrEmpty(swtchVal[0]))
                        {
                            //Value can be empty so verify it first
                            var tmp_swtch_val = (swtchVal[1] == null) ? null : swtchVal[1].Trim();

                            //Save the switch and value
                            result.Add(swtchVal[0].Trim(), tmp_swtch_val);
                        }
                    }
                    else
                    {
                        //If the assignment identifier is a space then we know switches can await values
                        if (_assignmentIdentifier == ' ')
                            awaitingSwitch = tempTrimmedArg.Trim();

                        //For now just add the switch maybe it wont be having any value
                        result.Add(tempTrimmedArg.Trim(), null);
                    }
                }
                else if (argument.Contains("/?"))
                {
                    //User can use standard help switch
                    result.Add("h", null);
                }
                else
                {
                    //If its not a switch then probably its a value for the last awaiting switch 
                    if (!string.IsNullOrEmpty(argument))
                    {
                        string tmpSwtchVal = argument.Trim();

                        ////Remove quotes if present
                        //if (tmp_swtch_val.StartsWith("\"") && tmp_swtch_val.EndsWith("\""))
                        //    tmp_swtch_val = tmp_swtch_val.Replace("\"", "");

                        //Check if there is a switch awaiting its value
                        //if so assign to it else mark as source
                        if (awaitingSwitch != null)
                            result[awaitingSwitch] = tmpSwtchVal;
                        else
                            source = tmpSwtchVal;
                    }

                    //Reset any awaiting switch
                    awaitingSwitch = null;
                }
            }

            //If source was found add source switch
            if (source != null)
                result.Add(SOURCE_SWITCH, source);

            return result;
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
        /// Creates a simple documentation.
        /// </summary>
        /// <param name="docTitle">Title for the document.</param>
        /// <param name="switchesWithDescr">Switchs and their descriptions.</param>
        /// <param name="usageExample">Usage example.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Arrgument cannot be null or emtpy.</exception>
        public string CreateDocumentation(string docTitle, Dictionary<string, string> switchesWithDescr, string usageExample = null)
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

            if (docTitle == null || switchesWithDescr == null || switchesWithDescr.Count == 0)
                throw new ArgumentException("Arrgument cannot be null or emtpy.");

            //Prepare format for the documentation
            string docFormat = "{0}" + Environment.NewLine +
                "Options : " + Environment.NewLine + "{1}" + Environment.NewLine +
                "Usage : " + Environment.NewLine + "{2}";

            string optionsFormat = "{0, -10} : {1}";
            string finalOptionsStr = "";

            //Prepare the list of all the options supported
            for (int i = 0; i < switchesWithDescr.Count; i++)
            {
                var kvp = switchesWithDescr.ElementAt(i);
                finalOptionsStr += string.Format(optionsFormat, kvp.Key, kvp.Value) + Environment.NewLine;
            }

            //If usage string has been provided use that else prepare one
            if (string.IsNullOrEmpty(usageExample))
            {
                if (_assignmentIdentifier == ' ')
                    usageExample = _switchIdentifier + "{option}" + "[SPACE]" + "{value}";
                else
                    usageExample = _switchIdentifier + "{option}" + _assignmentIdentifier + "{value}";
            }

            //Return ready documentation to user
            return string.Format(docFormat, docTitle, finalOptionsStr, usageExample);
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
