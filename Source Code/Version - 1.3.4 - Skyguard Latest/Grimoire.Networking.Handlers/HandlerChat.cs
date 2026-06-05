using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers;

public class HandlerChat : IXtMessageHandler
{
	public string[] HandledCommands { get; } = new string[3] { "chatm", "whisper", "server" };

	public void Handle(XtMessage message)
	{
		string text = "";
		switch (message.Arguments[2])
		{
		case "chatm":
			text = (message.Arguments[5] + message.Arguments[4]).Replace("zone~", ": ");
			break;
		case "whisper":
			text = ((message.Arguments[6] == Player.Username.ToLower()) ? ("From " + message.Arguments[5]) : ("To " + message.Arguments[6]));
			text = text + ": " + message.Arguments[4];
			break;
		case "server":
			text = message.Arguments[4];
			break;
		}
		text = text.Replace(Player.Username.ToLower(), "You");
		if (!string.IsNullOrEmpty(text))
		{
			LogForm.Instance.AppendChat($"[{DateTime.Now:hh:mm:ss}] {text}\r\n");
		}
	}

	void IXtMessageHandler.Handle(XtMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}

	public static async Task ResetAFKTimer()
	{
		while (Player.IsAfk)
		{
			Player.AFKPostpone();
			await Task.Delay(60000);
		}
	}
}
