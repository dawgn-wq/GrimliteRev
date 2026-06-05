using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				ResourceManager resourceManager = new ResourceManager("Grimoire.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static CultureInfo Culture
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

	public static string defaulttext => ResourceManager.GetString("defaulttext", resourceCulture);

	public static Bitmap GitHub
	{
		get
		{
			object obj = ResourceManager.GetObject("GitHub", resourceCulture);
			return (Bitmap)obj;
		}
	}

	public static Icon GrimoireIcon
	{
		get
		{
			object obj = ResourceManager.GetObject("GrimoireIcon", resourceCulture);
			return (Icon)obj;
		}
	}

	public static Bitmap GrimoireIconBig
	{
		get
		{
			object obj = ResourceManager.GetObject("GrimoireIconBig", resourceCulture);
			return (Bitmap)obj;
		}
	}

	public static Bitmap GrimoireIconBig1
	{
		get
		{
			object obj = ResourceManager.GetObject("GrimoireIconBig1", resourceCulture);
			return (Bitmap)obj;
		}
	}

	public static Bitmap MPGHfavicon
	{
		get
		{
			object obj = ResourceManager.GetObject("MPGHfavicon", resourceCulture);
			return (Bitmap)obj;
		}
	}

	public static Bitmap MPGHfavicon16
	{
		get
		{
			object obj = ResourceManager.GetObject("MPGHfavicon16", resourceCulture);
			return (Bitmap)obj;
		}
	}

	public static string statementcmds => ResourceManager.GetString("statementcmds", resourceCulture);

	internal Resources()
	{
	}
}
