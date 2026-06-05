using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Networking;

namespace Grimoire.Botting.Commands.Misc;

public class CmdPacket2 : RegularExpression, IBotCommand
{
	private string _packet;

	public string Packet { get; set; }

	public int Delay { get; set; }

	public bool Client { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		_packet = instance.Value(Packet).Replace("{ROOM_ID}", World.RoomId.ToString()).Replace("{ROOM_NUMBER}", World.RoomNumber.ToString())
			.Replace("PLAYERNAME", Player.Username)
			.Replace("{GETMAP}", Player.Map);
		_packet = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(_packet, (Match m) => new Random().Next(1001, 99999).ToString());
		await (Client ? Proxy.Instance.SendToClient(_packet) : Proxy.Instance.SendToServer(_packet));
		await Task.Delay(Delay);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return (Client ? "Send packet to client: " : "Send packet to server: ") + Packet;
	}
}
