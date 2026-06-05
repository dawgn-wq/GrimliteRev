using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Networking;

namespace Grimoire.Botting.Commands.Misc;

public class CmdPacket : RegularExpression, IBotCommand
{
	public string Packet { get; set; }

	public bool Client { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		string text = ((!IsVar(Packet)) ? Packet : Configuration.Tempvariable[GetVar(Packet)]);
		text = text.Replace("{ROOM_ID}", World.RoomId.ToString()).Replace("{ROOM_NUMBER}", World.RoomNumber.ToString()).Replace("PLAYERNAME", Player.Username);
		text = text.Replace("{GETMAP}", Player.Map);
		while (text.Contains("--"))
		{
			text = new Regex("-{1,}", RegexOptions.IgnoreCase).Replace(text, (Match m) => "-");
		}
		text = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(text, (Match m) => new Random().Next(1001, 99999).ToString());
		if (!Client)
		{
			await Proxy.Instance.SendToServer(text);
		}
		else
		{
			await Proxy.Instance.SendToClient(text);
		}
		if (text.Contains("%xt%zm%gar%"))
		{
			await Task.Delay(700);
		}
		else
		{
			await Task.Delay(2000);
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return (Client ? "Send client packet: " : "Send server packet: ") + Packet;
	}
}
