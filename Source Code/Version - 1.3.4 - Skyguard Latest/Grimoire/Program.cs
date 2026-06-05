using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using Grimoire.UI;
using Microsoft.Win32;

namespace Grimoire;

internal static class Program
{
	public static string versionDate = "14072022";

	public static string flashVersion;

	public static bool privateBuild { get; set; } = false;

	public static bool access { get; set; }

	[STAThread]
	private static void Main()
	{
		access = Access();
		flashVersion = FlashVersion();
		if (privateBuild && !access)
		{
			DarkMessageBox.Show(new Form
			{
				TopMost = true,
				StartPosition = FormStartPosition.CenterScreen
			}, "Access denied.", "Grimlite Rev v1.3", MessageBoxIcon.Hand);
		}
		else
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new Root());
		}
	}

	private static bool Access()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		HttpClient val = new HttpClient();
		ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
		try
		{
			((HttpHeaders)val.DefaultRequestHeaders).Add("user-agent", "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)");
			string result = val.GetStringAsync(privateBuild ? "https://gitfront.io/r/GentleGanku/75b44be5e2626c1af385e2911c99e94367a4b7df/GrimliteRev-Internal/" : "https://github.com/GentleGanku/GrimliteRev/releases/latest").Result;
			if (privateBuild)
			{
				string value = Regex.Match(result, "\\<title\\b[^>]*\\>\\s*(?<Title>[\\s\\S]*?)\\</title\\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
				return value.Contains("GrimliteRev-Internal") ? true : false;
			}
			return result.Contains("(" + versionDate + ")") ? true : false;
		}
		catch
		{
			return !privateBuild;
		}
	}

	public static string FlashVersion()
	{
		RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("ShockwaveFlash.ShockwaveFlash\\CurVer");
		if (registryKey != null)
		{
			return registryKey.GetValue(null) as string;
		}
		return string.Empty;
	}

	public static async void CheckVersion()
	{
		if (!privateBuild && !access)
		{
			await Task.Delay(250);
			DialogResult dialogResult = DarkMessageBox.Show(new Form
			{
				TopMost = true,
				StartPosition = FormStartPosition.CenterScreen
			}, "Outdated Build issue has been detected. Would you like to download the latest version?", "Grimlite Rev v1.3 (" + versionDate + ")", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
			if (dialogResult == DialogResult.Yes)
			{
				Process.Start("https://github.com/GentleGanku/GrimliteRev/releases/latest");
			}
		}
	}

	public static bool CheckFlashVersion()
	{
		if (flashVersion == string.Empty)
		{
			DarkMessageBox.Show(new Form
			{
				TopMost = true,
				StartPosition = FormStartPosition.CenterScreen
			}, "You may have to install the Adobe Flash Player plugin (for ActiveX) in order for the bot client to load the game.", "Flash Player", MessageBoxIcon.Hand);
			return true;
		}
		return false;
	}
}
