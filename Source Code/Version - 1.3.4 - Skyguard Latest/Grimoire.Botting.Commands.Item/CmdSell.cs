using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Item;

public class CmdSell : IBotCommand
{
	public string ItemName { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Transaction;
		await Shop.SellItem(instance.Value(ItemName));
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Sell: " + ItemName;
	}
}
