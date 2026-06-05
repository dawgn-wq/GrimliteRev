using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item;

public class CmdPacketlist2 : IBotCommand
{
	public int Type { get; set; }

	public string Packet { get; set; }

	public int Delay { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		switch (Type)
		{
		case 0:
			OptionsManager.PacketSpam = false;
			break;
		case 1:
			OptionsManager.PacketSpam = true;
			break;
		default:
			OptionsManager.PacketSpam = !OptionsManager.PacketSpam;
			break;
		}
		OptionsManager.SpamPacket = Packet;
		OptionsManager.PacketDelay = Delay;
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return Type switch
		{
			0 => "Spam packet: Off", 
			1 => "Spam packet: On, " + Packet, 
			_ => "Spam packet", 
		};
	}
}
