﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPUtils.Core.v02 {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SPUtils.Core.v02.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;div&gt;
        ///    &lt;table&gt;
        ///        &lt;tr&gt;
        ///            &lt;td&gt;Type&lt;/td&gt;
        ///            &lt;td&gt;:&lt;/td&gt;
        ///            &lt;td&gt;{0}&lt;/td&gt;
        ///        &lt;/tr&gt;
        ///        &lt;tr&gt;
        ///            &lt;td&gt;Message&lt;/td&gt;
        ///            &lt;td&gt;:&lt;/td&gt;
        ///            &lt;td&gt;{1}&lt;/td&gt;
        ///        &lt;/tr&gt;
        ///        &lt;tr&gt;
        ///            &lt;td&gt;Line no.&lt;/td&gt;
        ///            &lt;td&gt;:&lt;/td&gt;
        ///            &lt;td&gt;{2}&lt;/td&gt;
        ///        &lt;/tr&gt;
        ///        &lt;tr&gt;
        ///            &lt;td&gt;Function&lt;/td&gt;
        ///            &lt;td&gt;:&lt;/td&gt;
        ///            &lt;td&gt;{3}&lt;/td&gt;
        ///        &lt;/tr&gt;
        ///        &lt;tr&gt;
        ///            &lt;td&gt;File&lt;/td&gt;
        ///            &lt;td&gt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ExceptionReportDiv {
            get {
                return ResourceManager.GetString("ExceptionReportDiv", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;HTML&gt;
        ///  &lt;BODY&gt;
        ///  
        ///  &lt;div&gt;
        ///      &lt;table&gt;
        ///          &lt;tr&gt;
        ///              &lt;td&gt;Application name&lt;/td&gt;
        ///              &lt;td&gt;:&lt;/td&gt;
        ///              &lt;td&gt;{0}&lt;/td&gt; 
        ///          &lt;/tr&gt;
        ///          &lt;tr&gt;
        ///              &lt;td&gt;Version&lt;/td&gt;
        ///              &lt;td&gt;:&lt;/td&gt;
        ///              &lt;td&gt;{1}&lt;/td&gt;
        ///          &lt;/tr&gt;
        ///          &lt;tr&gt;
        ///              &lt;td&gt;Tag&lt;/td&gt;
        ///              &lt;td&gt;:&lt;/td&gt;
        ///              &lt;td&gt;{2}&lt;/td&gt;
        ///          &lt;/tr&gt;
        ///          &lt;tr&gt;
        ///              &lt;td&gt;Error&lt;/td&gt;
        ///              &lt;td&gt;:&lt;/td&gt;
        ///              &lt;td&gt;{3}&lt;/td&gt;
        ///    [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HtmlErrorFormat {
            get {
                return ResourceManager.GetString("HtmlErrorFormat", resourceCulture);
            }
        }
    }
}