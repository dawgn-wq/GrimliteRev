using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Item;

public class CmdBuyFast : IBotCommand
{
	public string ItemName { get; set; }

	public bool Manual { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Transaction;
		await Shop.BuyItem(instance.Value(ItemName), Manual);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Buy item fast: " + ItemName;
	}
}
