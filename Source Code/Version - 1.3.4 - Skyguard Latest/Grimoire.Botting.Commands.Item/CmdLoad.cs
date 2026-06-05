using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Item;

public class CmdLoad : IBotCommand
{
	public int ShopId { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Transaction;
		await Shop.Load(ShopId);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Load Shop: " + ShopId;
	}
}
