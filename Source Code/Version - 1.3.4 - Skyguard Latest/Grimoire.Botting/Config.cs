using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting;

public class Config
{
	public string file { get; set; }

	public Dictionary<string, string> Contents { get; set; } = new Dictionary<string, string>();

	public static Config Instance { get; set; } = Load(Application.StartupPath + "\\config.cfg");

	public string Get(string key)
	{
		if (!Contents.TryGetValue(key, out var value))
		{
			return null;
		}
		return value;
	}

	public void Set(string key, string value)
	{
		Contents[key] = value;
	}

	public void Save()
	{
		File.WriteAllLines(file, Contents.Select((KeyValuePair<string, string> kvp) => kvp.Key + "=" + kvp.Value));
	}

	public static Config Load(string path)
	{
		if (!File.Exists(path))
		{
			File.Create(path);
			Task.Delay(100);
		}
		return new Config
		{
			file = path,
			Contents = (from l in File.ReadLines(path)
				select l.Split('=')).ToDictionary((string[] a) => a[0], (string[] a) => a[1])
		};
	}

	public T GetValue<T>(string key)
	{
		object obj;
		try
		{
			obj = Instance.Get(key);
		}
		catch
		{
			obj = null;
		}
		if (typeof(T) == typeof(int))
		{
			obj = ((obj == null) ? (-1) : int.Parse(obj.ToString()));
		}
		else if (typeof(T) == typeof(bool))
		{
			obj = obj != null && bool.Parse(obj.ToString());
		}
		else if (typeof(T) == typeof(string))
		{
			obj = obj?.ToString();
		}
		return (T)obj;
	}

	public void SetValue(string key, string value)
	{
		Instance.Set(key, value);
		Instance.Save();
	}
}
