namespace Grimoire.Properties
{
    using System;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources()
        {
        }

        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Grimoire.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        public static string defaulttext
        {
            get
            {
                return ResourceManager.GetString("defaulttext", resourceCulture);
            }
        }

        public static System.Drawing.Bitmap GitHub
        {
            get
            {
                object obj = ResourceManager.GetObject("GitHub", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }

        public static System.Drawing.Icon GrimoireIcon
        {
            get
            {
                object obj = ResourceManager.GetObject("GrimoireIcon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }

        public static System.Drawing.Bitmap GrimoireIconBig
        {
            get
            {
                object obj = ResourceManager.GetObject("GrimoireIconBig", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }

        public static System.Drawing.Bitmap GrimoireIconBig1
        {
            get
            {
                object obj = ResourceManager.GetObject("GrimoireIconBig1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }

        public static System.Drawing.Bitmap MPGHfavicon
        {
            get
            {
                object obj = ResourceManager.GetObject("MPGHfavicon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }

        public static System.Drawing.Bitmap MPGHfavicon16
        {
            get
            {
                object obj = ResourceManager.GetObject("MPGHfavicon16", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }

        public static string statementcmds
        {
            get
            {
                return ResourceManager.GetString("statementcmds", resourceCulture);
            }
        }
    }
}