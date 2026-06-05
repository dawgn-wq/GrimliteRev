using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Item;

public class CmdGetDrop : IBotCommand
{
	public string ItemName { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Others;
		await World.DropStack.GetDrop(instance.Value(ItemName));
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Get drop: " + ItemName;
	}
}
