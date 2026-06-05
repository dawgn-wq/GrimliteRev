using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Item;

public class CmdBuyFastByID : IBotCommand
{
	public string ItemID { get; set; }

	public string ShopID { get; set; }

	public string ShopItemID { get; set; }

	public bool Manual { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Transaction;
		await Shop.BuyItemById(int.Parse(instance.Value(ItemID)), int.Parse(instance.Value(ShopID)), int.Parse(instance.Value(ShopItemID)), Manual);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Buy item fast by ID: " + ItemID + ", " + ShopID + ", " + ShopItemID;
	}
}
