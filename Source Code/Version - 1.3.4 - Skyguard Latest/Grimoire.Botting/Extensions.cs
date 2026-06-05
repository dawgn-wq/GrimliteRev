using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Grimoire.Botting;

public static class Extensions
{
	public class Line : IComparable<Line>
	{
		private int _number;

		private string _afterNumber;

		private readonly string _line;

		public Line(string line)
		{
			int num = line.IndexOf(' ');
			string s = line.Substring(0, num);
			_number = int.Parse(s);
			_afterNumber = line.Substring(num);
			_line = line;
		}

		public int CompareTo(Line other)
		{
			int num = _number.CompareTo(other._number);
			if (num != 0)
			{
				return num;
			}
			return _afterNumber.CompareTo(other._afterNumber);
		}

		int IComparable<Line>.CompareTo(Line other)
		{
			//ILSpy generated this explicit interface implementation from .override directive in CompareTo
			return this.CompareTo(other);
		}

		public override string ToString()
		{
			return _line;
		}
	}

	public static string Correct(this string str)
	{
		if (str.Contains("<false/>") || str.Contains("<true/>"))
		{
			return str.Replace("<false/>", bool.FalseString).Replace("<true/>", bool.TrueString);
		}
		return str;
	}

	public static string ReplaceLink(this string str)
	{
		return str.Replace(".swf", "").Replace("_skin", "");
	}

	public static void AppendText(this RichTextBox box, string text, Color color)
	{
		box.SelectionStart = box.TextLength;
		box.SelectionLength = 0;
		box.SelectionColor = color;
		box.AppendText(text);
		box.SelectionColor = box.ForeColor;
	}

	public static string FromBase64(this string str)
	{
		return Encoding.UTF8.GetString(Convert.FromBase64String(str));
	}

	public static string generatePastelHex(Random random, int mixR, int mixG, int mixB)
	{
		int num = random.Next(256);
		int num2 = random.Next(256);
		int num3 = random.Next(256);
		num = (num + mixR) / 2;
		num2 = (num2 + mixG) / 2;
		num3 = (num3 + mixB) / 2;
		return $"FF{num:X2}{num2:X2}{num3:X2}";
	}

	public static string SanitizeXml(this string str)
	{
		return str.Replace("&apos;", "'").Replace("&amp;", "&");
	}

	public static string NullIfEmpty(this string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			return s;
		}
		return null;
	}

	public static string NullIfWhiteSpace(this string s)
	{
		if (!string.IsNullOrWhiteSpace(s))
		{
			return s;
		}
		return null;
	}

	public static string MakeRelativePath(string fromPath, string toPath)
	{
		if (string.IsNullOrEmpty(fromPath))
		{
			throw new ArgumentNullException("fromPath");
		}
		if (string.IsNullOrEmpty(toPath))
		{
			throw new ArgumentNullException("toPath");
		}
		Uri uri = new Uri(fromPath);
		Uri uri2 = new Uri(toPath);
		if (uri.Scheme != uri2.Scheme)
		{
			return toPath;
		}
		Uri uri3 = uri.MakeRelativeUri(uri2);
		string text = Uri.UnescapeDataString(uri3.ToString());
		if (uri2.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
		{
			text = text.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}
		return text;
	}

	public static string MakeRelativePathFrom(string fromPath, string toPath)
	{
		if (string.IsNullOrEmpty(fromPath))
		{
			throw new ArgumentNullException("fromPath");
		}
		if (string.IsNullOrEmpty(toPath))
		{
			throw new ArgumentNullException("toPath");
		}
		Uri uri = new Uri(fromPath);
		Uri uri2 = new Uri(toPath);
		if (uri.Scheme != uri2.Scheme)
		{
			return toPath;
		}
		Uri uri3 = uri.MakeRelativeUri(uri2);
		string text = Uri.UnescapeDataString(uri3.ToString());
		if (uri2.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
		{
			text = text.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}
		string[] array = text.Split(new string[1] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
		return text.Replace(array[0] + "\\", "");
	}

	public static string[] JtoArray(string result)
	{
		return JsonConvert.DeserializeObject<string[]>(result);
	}

	public static string Base64Encode(string plainText)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(plainText);
		return Convert.ToBase64String(bytes);
	}

	public static string Base64Decode(string base64EncodedData)
	{
		byte[] bytes = Convert.FromBase64String(base64EncodedData);
		return Encoding.UTF8.GetString(bytes);
	}

	public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
	{
		if (control.InvokeRequired)
		{
			control.BeginInvoke(action);
		}
		else
		{
			action();
		}
	}

	public static float ToSingle(double value)
	{
		return (float)value;
	}
}
