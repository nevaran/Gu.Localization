﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gu.Localization.Properties {
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    public class Resources {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("Gu.Localization.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to !{0}!.
        /// </summary>
        public static string MissingKeyFormat {
            get {
                return ResourceManager.GetString("MissingKeyFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ?{0}?.
        /// </summary>
        public static string MissingResourcesFormat {
            get {
                return ResourceManager.GetString("MissingResourcesFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -{0}-.
        /// </summary>
        public static string MissingTranslationFormat {
            get {
                return ResourceManager.GetString("MissingTranslationFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ~{0}~.
        /// </summary>
        public static string NoLanguagesFormat {
            get {
                return ResourceManager.GetString("NoLanguagesFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ${0}$.
        /// </summary>
        public static string NullManagerFormat {
            get {
                return ResourceManager.GetString("NullManagerFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #{0}#.
        /// </summary>
        public static string UnknownErrorFormat {
            get {
                return ResourceManager.GetString("UnknownErrorFormat", resourceCulture);
            }
        }
    }
}