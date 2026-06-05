using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using Grimoire.UI;

namespace Grimoire.Networking;

public class Proxy
{
	public delegate void Receive(Message message);

	private List<IJsonMessageHandler> _handlersJson;

	private List<IXtMessageHandler> _handlersXt;

	private List<IXmlMessageHandler> _handlersXml;

	public static CancellationTokenSource AppClosingToken;

	public bool _catchXtPackets { get; set; }

	public bool _catchAllPackets { get; set; }

	public bool firstJoin { get; set; } = true;

	public static Proxy Instance { get; set; }

	public static event Receive ReceivedFromGame;

	private Proxy()
	{
	}

	protected internal static void InitializeProxyHandlers()
	{
		Instance = new Proxy();
		AppClosingToken = new CancellationTokenSource();
		Instance._handlersJson = new List<IJsonMessageHandler>
		{
			new HandlerDrops(),
			new HandlerGetQuests(),
			new HandlerQuestComplete(),
			new HandlerLoadShop(),
			new HandlerBuyItem(),
			new HandlerSkills(),
			new HandlerArea(),
			new HandlerBotSkills()
		};
		Instance._handlersXt = new List<IXtMessageHandler>
		{
			new HandlerAFK(),
			new HandlerWarningsXt(),
			new HandlerLogin(),
			new HandlerChat(),
			new HandlerXtJoin(),
			new HandlerXtCellJoin(),
			new HandlerConnection()
		};
		Instance._handlersXml = new List<IXmlMessageHandler>();
		ReceivedFromGame += Instance.ProcessMessage;
	}

	public void RegisterHandler(IJsonMessageHandler handler)
	{
		RegisterHandler(handler, _handlersJson);
	}

	public void RegisterHandler(IXmlMessageHandler handler)
	{
		RegisterHandler(handler, _handlersXml);
	}

	public void RegisterHandler(IXtMessageHandler handler)
	{
		RegisterHandler(handler, _handlersXt);
	}

	public void UnregisterHandler(IJsonMessageHandler handler)
	{
		_handlersJson.Remove(handler);
	}

	public void UnregisterHandler(IXmlMessageHandler handler)
	{
		_handlersXml.Remove(handler);
	}

	public void UnregisterHandler(IXtMessageHandler handler)
	{
		_handlersXt.Remove(handler);
	}

	private void RegisterHandler<T>(T handler, List<T> list)
	{
		if (!list.Contains(handler))
		{
			list.Add(handler);
		}
	}

	public async Task SendToServer(string data)
	{
		string text = data.Replace("{ROOM_ID}", World.RoomId.ToString());
		if (text != null && text.Length > 0)
		{
			bool flag = data.StartsWith("{");
			World.SendPacket(data, flag ? "Json" : "String");
		}
	}

	public async Task SendToClient(string data)
	{
		if (data != null && data.Length > 0)
		{
			bool flag = data.StartsWith("{");
			World.SendClientPacket(data, flag ? "json" : "str");
		}
	}

	private void ProcessMessage(Message message)
	{
		try
		{
			XtMessage xtMessage = message as XtMessage;
			if (xtMessage == null)
			{
				JsonMessage jsonMessage = message as JsonMessage;
				if (jsonMessage == null)
				{
					XmlMessage xmlMessage = message as XmlMessage;
					if (xmlMessage == null)
					{
						return;
					}
					{
						foreach (IXmlMessageHandler item in Instance._handlersXml.Where((IXmlMessageHandler h) => h.HandledCommands.Contains(xmlMessage.Command)))
						{
							item.Handle(xmlMessage);
						}
						return;
					}
				}
				{
					foreach (IJsonMessageHandler item2 in Instance._handlersJson.Where((IJsonMessageHandler h) => h.HandledCommands.Contains(jsonMessage.Command)))
					{
						item2.Handle(jsonMessage);
					}
					return;
				}
			}
			foreach (IXtMessageHandler item3 in Instance._handlersXt.Where((IXtMessageHandler h) => h.HandledCommands.Contains(xtMessage.Command)))
			{
				item3.Handle(xtMessage);
			}
		}
		catch
		{
		}
	}

	private Message CreateMessage(string raw)
	{
		if (raw != null && raw.Length > 0)
		{
			switch (raw[0])
			{
			case '<':
				return new XmlMessage(raw);
			case '%':
				return new XtMessage(raw);
			case '{':
				return new JsonMessage(raw);
			}
		}
		return null;
	}

	static Proxy()
	{
	}

	public static async void DisableBotSkill()
	{
		if (BotUtilities.ShouldUseSkill)
		{
			await Task.Delay(10);
			BotUtilities.ShouldUseSkill = false;
			Player.CancelAutoAttack();
			World.GamePopup("Disabled the bot's ability to attack.");
		}
	}

	public static async void EnableBotSkill()
	{
		if (!BotUtilities.ShouldUseSkill)
		{
			await Task.Delay(10);
			BotUtilities.ShouldUseSkill = true;
			World.GamePopup("Enabled the bot's ability to attack.");
		}
	}

	public static void OnPingLatency(int latency)
	{
		if (Root.Instance.rtbPing.Visible)
		{
			if (latency >= 0 && latency <= 100)
			{
				Root.Instance.rtbPing.ForeColor = Color.LimeGreen;
			}
			else if (latency >= 101 && latency <= 200)
			{
				Root.Instance.rtbPing.ForeColor = Color.Orange;
			}
			else if (latency >= 201)
			{
				Root.Instance.rtbPing.ForeColor = Color.Red;
			}
			Root.Instance.rtbPing.Clear();
			Root.Instance.rtbPing.AppendText($"⬤  {latency} ms");
		}
	}

	public async Task ClientExecute()
	{
		while (!AppClosingToken.IsCancellationRequested)
		{
			if (Player.IsLoggedIn)
			{
				if (BotManager.Instance.ActiveBotEngine.IsRunning && BotManager.Instance.ActiveBotEngine.Configuration.EnableRejection && !World.VisibleDropUI)
				{
					Flash.Call("dropUIOpt", false);
				}
			}
			else if (Root.Instance.rtbPing.Text != "")
			{
				Root.Instance.rtbPing.Clear();
			}
			await Task.Delay(100);
		}
	}

	public void LogPackets(string text, string text2)
	{
		switch (text)
		{
		case "packet":
			Proxy.ReceivedFromGame(CreateMessage(text2));
			if (text2.Contains("<msg t='sys'><body action='logout' r='0'></body></msg>"))
			{
				LogForm.Instance.AppendDebug($"[{DateTime.Now:hh:mm:ss}] Safely logged out from the game.\r\n");
				Player.IsLoggedIn = false;
			}
			if (_catchAllPackets)
			{
				PacketTamperer.Instance.txtReceive.AppendText((text2.StartsWith("%xt%zm%") ? "From client: " : "From server: ") + text2 + "\r\n" + (text2.StartsWith("%xt%zm%") ? "" : "\r\n"));
			}
			break;
		case "xtPacket":
			if (_catchXtPackets)
			{
				PacketLogger.Instance.txtPackets.AppendText(text2 + "\r\n");
			}
			break;
		case "ping":
			OnPingLatency(int.Parse(text2));
			break;
		case "chatCommand":
			ChatCommand.ExecuteCommand(text2);
			break;
		case "openWebsite":
			Process.Start(text2);
			break;
		}
	}
}
