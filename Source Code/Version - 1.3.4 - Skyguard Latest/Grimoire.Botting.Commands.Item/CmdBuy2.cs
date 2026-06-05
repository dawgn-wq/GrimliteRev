using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Item;

public class CmdBuy2 : IBotCommand
{
	public string ShopId { get; set; }

	public string ItemName { get; set; }

	public bool Manual { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Transaction;
		await Shop.Load(int.Parse(instance.Value(ShopId)));
		await Shop.BuyItem(instance.Value(ItemName), Manual);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Buy item: " + ItemName;
	}
}
