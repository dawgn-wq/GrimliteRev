using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Xml.Linq;
using AxShockwaveFlashObjects;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.UI;
using Grimoire.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grimoire.Tools;

public class Flash
{
	private static Flash _instance;

	public static AxShockwaveFlash flash;

	public static Flash Instance => _instance ?? (_instance = new Flash());

	public static event FlashCallHandler FlashCall;

	public static event FlashErrorHandler FlashError;

	public static event Action<int> SwfLoadProgress;

	public static void ProcessFlashCall(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
	{
		XElement xElement = XElement.Parse(e.request);
		string text = xElement.Attribute("name")?.Value;
		string text2 = xElement.Element("arguments")?.Value;
		if (text == null)
		{
			return;
		}
		if (!(text == "progress"))
		{
			if (text == "modifyServers")
			{
				Root.Instance.Client.SetReturnValue("<string>" + ModifyServerList(text2.Trim()) + "</string>");
			}
			Proxy.Instance.LogPackets(text, text2);
		}
		else
		{
			Flash.SwfLoadProgress?.Invoke(int.Parse(text2));
		}
	}

	public string GetGameObject(string path)
	{
		return Call<string>("getGameObject", new string[1] { path });
	}

	public string GetGameObjectStatic(string path)
	{
		return Call<string>("getGameObjectS", new string[1] { path });
	}

	public T GetGameObject<T>(string path, T def = default(T))
	{
		try
		{
			return JsonConvert.DeserializeObject<T>(GetGameObject(path));
		}
		catch
		{
			return def;
		}
	}

	public static string CallGameFunction(string path, params object[] args)
	{
		if (args.Length == 0)
		{
			return Call<string>("callGameFunction0", new string[1] { path });
		}
		return Call("callGameFunction", new object[1] { path }.Concat(args).ToArray());
	}

	public void SetGameObject(string path, object value)
	{
		Call("setGameObject", path, value);
	}

	public static string Call(string function, params object[] args)
	{
		return Call<string>(function, args);
	}

	public static T Call<T>(string function, params object[] args)
	{
		try
		{
			return (T)Call(function, typeof(T), args);
		}
		catch
		{
			return default(T);
		}
	}

	public static object Call(string function, Type type, params object[] args)
	{
		try
		{
			StringBuilder req = new StringBuilder().Append("<invoke name=\"" + function + "\" returntype=\"xml\">");
			if (args.Length != 0)
			{
				req.Append("<arguments>");
				args.ForEach(delegate(object o)
				{
					req.Append(ToFlashXml(o));
				});
				req.Append("</arguments>");
			}
			req.Append("</invoke>");
			string text = flash.CallFunction(req.ToString());
			XElement xElement = XElement.Parse(text);
			return (xElement == null || xElement.FirstNode == null) ? null : Convert.ChangeType(xElement.FirstNode.ToString(), type);
		}
		catch (Exception e)
		{
			Flash.FlashError?.Invoke(flash, e, function, args);
			return null;
		}
	}

	public static T Call<T>(string function, params string[] args)
	{
		return TryDeserialize<T>(GetResponse(BuildRequest(function, args)));
	}

	public static string CallString(string function, params object[] args)
	{
		return Call<string>(function, args);
	}

	public static void Call(string function, params string[] args)
	{
		Call<string>(function, args);
	}

	private static string BuildRequest(string method, params string[] args)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<invoke name=\"" + method + "\" returntype=\"xml\">");
		if (args != null && args.Length != 0)
		{
			stringBuilder.Append("<arguments>");
			foreach (string text in args)
			{
				stringBuilder.Append("<string>" + text + "</string>");
			}
			stringBuilder.Append("</arguments>");
		}
		stringBuilder.Append("</invoke>");
		return stringBuilder.ToString();
	}

	private static string GetResponse(string request)
	{
		try
		{
			return HttpUtility.HtmlDecode(XElement.Parse(Root.Instance.Client.CallFunction(request)).FirstNode?.ToString() ?? string.Empty);
		}
		catch
		{
			return string.Empty;
		}
	}

	private static T TryDeserialize<T>(string str)
	{
		try
		{
			return JsonConvert.DeserializeObject<T>(str);
		}
		catch
		{
			return default(T);
		}
	}

	public static string ToFlashXml(object o)
	{
		if (o != null)
		{
			if (!(o is bool))
			{
				if (!(o is double) && !(o is float) && !(o is long) && !(o is int))
				{
					if (o is ExpandoObject)
					{
						StringBuilder stringBuilder = new StringBuilder().Append("<object>");
						foreach (KeyValuePair<string, object> item in o as IDictionary<string, object>)
						{
							stringBuilder.Append("<property id=\"" + item.Key + "\">" + ToFlashXml(item.Value) + "</property>");
						}
						return stringBuilder.Append("</object>").ToString();
					}
					if (o is Array)
					{
						StringBuilder stringBuilder2 = new StringBuilder().Append("<array>");
						int num = 0;
						foreach (object item2 in o as Array)
						{
							stringBuilder2.Append($"<property id=\"{num++}\">{ToFlashXml(item2)}</property>");
						}
						return stringBuilder2.Append("</array>").ToString();
					}
					return "<string>" + SecurityElement.Escape(o.ToString()) + "</string>";
				}
				return $"<number>{o}</number>";
			}
			return "<" + o.ToString().ToLower() + "/>";
		}
		return "<null/>";
	}

	private static string ModifyServerList(string response)
	{
		if (response.StartsWith("{\"login\"") && response.EndsWith("}"))
		{
			return ServersFromJson(response);
		}
		return response;
	}

	private static string ServersFromJson(string json)
	{
		JObject jObject = JObject.Parse(json);
		JObject jObject2 = (JObject)jObject["login"];
		JArray jArray = (JArray)jObject["servers"];
		Server[] array = new Server[jArray.Count];
		jObject2["iAge"] = 99;
		for (int i = 0; i < jArray.Count; i++)
		{
			JObject jObject3 = (JObject)jArray[i];
			array[i] = new Server
			{
				IsChatRestricted = (jObject3.GetValue("iChat")?.ToString() == "0"),
				PlayerCount = int.Parse(jObject3.GetValue("iCount")?.ToString()),
				MaxCount = int.Parse(jObject3.GetValue("iMax")?.ToString()),
				IsMemberOnly = (jObject3.GetValue("bUpg")?.ToString() == "1"),
				IsOnline = (jObject3.GetValue("bOnline")?.ToString() == "1"),
				Name = jObject3.GetValue("sName")?.ToString(),
				Language = jObject3.GetValue("sLang")?.ToString(),
				Port = int.Parse(jObject3.GetValue("iPort")?.ToString()),
				Ip = jObject3.GetValue("sIP")?.ToString()
			};
		}
		BotManager.Instance.OnServersLoaded(array);
		ParseAvatarDatas(jObject2);
		return jObject.ToString(Formatting.None);
	}

	private static void ParseAvatarDatas(JObject loginResponse)
	{
		Player.Username = loginResponse["unm"].ToString();
		Player.CharUserID = int.Parse(loginResponse["userid"].ToString());
	}

	public static object FromFlashXml(XElement el)
	{
		switch (el.Name.ToString())
		{
		case "number":
		{
			int result;
			float result2;
			return int.TryParse(el.Value, out result) ? ((float)result) : (float.TryParse(el.Value, out result2) ? result2 : 0f);
		}
		case "true":
			return true;
		case "false":
			return false;
		case "null":
			return null;
		case "array":
			return (from e in el.Elements()
				select FromFlashXml(e)).ToArray();
		case "object":
		{
			dynamic d = new ExpandoObject();
			el.Elements().ForEach(delegate(XElement e)
			{
				d[e.Attribute("id").Value] = FromFlashXml(e.Elements().First());
			});
			return d;
		}
		default:
			return el.Value;
		}
	}

	public static void CallHandler(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
	{
		XElement xElement = XElement.Parse(e.request);
		string value = xElement.Attribute("name").Value;
		object[] args = (from x in xElement.Elements()
			select FromFlashXml(x)).ToArray();
		Flash.FlashCall?.Invoke(flash, value, args);
	}
}
