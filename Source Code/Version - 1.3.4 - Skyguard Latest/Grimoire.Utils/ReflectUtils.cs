using System;
using System.IO;
using System.Reflection;

namespace Grimoire.Utils;

public static class ReflectUtils
{
	public static object GetDefaultValue(this Type type)
	{
		if (!(type != typeof(void)) || !(type != null) || !type.IsValueType)
		{
			return null;
		}
		return Activator.CreateInstance(type);
	}

	public static bool IsCompatible(this ProcessorArchitecture arch)
	{
		if (arch != ProcessorArchitecture.MSIL)
		{
			return Assembly.GetExecutingAssembly().GetName().ProcessorArchitecture == arch;
		}
		return true;
	}

	public static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
	{
		string location = assembly.Location;
		byte[] array = new byte[2048];
		using (FileStream fileStream = new FileStream(location, FileMode.Open, FileAccess.Read))
		{
			fileStream.Read(array, 0, 2048);
		}
		int num = BitConverter.ToInt32(array, 60);
		int num2 = BitConverter.ToInt32(array, num + 8);
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(num2);
		TimeZoneInfo destinationTimeZone = target ?? TimeZoneInfo.Local;
		return TimeZoneInfo.ConvertTimeFromUtc(dateTime, destinationTimeZone);
	}
}
