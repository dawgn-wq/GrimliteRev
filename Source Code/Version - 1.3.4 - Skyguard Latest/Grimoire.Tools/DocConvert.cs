using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Grimoire.Tools;

public static class DocConvert
{
	public static bool IsBase64Encoded(string str)
	{
		try
		{
			byte[] array = Convert.FromBase64String(str);
			return str.Replace(" ", "").Length % 4 == 0;
		}
		catch
		{
			return false;
		}
	}

	public static void CopyTo(Stream src, Stream dest)
	{
		byte[] array = new byte[4096];
		int count;
		while ((count = src.Read(array, 0, array.Length)) != 0)
		{
			dest.Write(array, 0, count);
		}
	}

	public static byte[] Zip(string str)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		using MemoryStream src = new MemoryStream(bytes);
		using MemoryStream memoryStream = new MemoryStream();
		using (GZipStream dest = new GZipStream(memoryStream, CompressionMode.Compress))
		{
			CopyTo(src, dest);
		}
		return memoryStream.ToArray();
	}

	public static string Zip<T>(string str)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		using MemoryStream src = new MemoryStream(bytes);
		using MemoryStream memoryStream = new MemoryStream();
		using (GZipStream dest = new GZipStream(memoryStream, CompressionLevel.Optimal))
		{
			CopyTo(src, dest);
		}
		return Convert.ToBase64String(memoryStream.ToArray());
	}

	public static string Unzip(byte[] bytes)
	{
		using MemoryStream stream = new MemoryStream(bytes);
		using MemoryStream memoryStream = new MemoryStream();
		using (GZipStream src = new GZipStream(stream, CompressionMode.Decompress))
		{
			CopyTo(src, memoryStream);
		}
		return Encoding.UTF8.GetString(memoryStream.ToArray());
	}

	public static string Unzip(string str)
	{
		byte[] buffer = Convert.FromBase64String(str);
		using MemoryStream stream = new MemoryStream(buffer);
		using MemoryStream memoryStream = new MemoryStream();
		using (GZipStream src = new GZipStream(stream, CompressionMode.Decompress))
		{
			CopyTo(src, memoryStream);
		}
		return Encoding.UTF8.GetString(memoryStream.ToArray());
	}

	public static Task toRtf(string source, string target, out string output)
	{
		Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
		object FileName = source;
		object FileName2 = target;
		object ConfirmConversions = Type.Missing;
		Documents documents = application.Documents;
		object XMLTransform = Type.Missing;
		documents.Open(ref FileName, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref XMLTransform);
		object FileFormat = WdSaveFormat.wdFormatRTF;
		application.ActiveDocument.SaveAs(ref FileName2, ref FileFormat, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions);
		application.Quit(ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions);
		output = target + ".rtf";
		return Task.FromResult(0);
	}
}
