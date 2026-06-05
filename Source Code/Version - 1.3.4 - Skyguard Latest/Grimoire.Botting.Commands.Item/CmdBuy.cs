using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Item;

public class CmdBuy : IBotCommand
{
	public int ShopId { get; set; }

	public string ItemName { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Transaction;
		await instance.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem), null, 10, 500);
		await BotUtilities.MoveOutOfCombat();
		Shop.ResetShopInfo();
		Shop.Load(ShopId);
		await instance.WaitUntil(() => Shop.IsShopLoaded);
		await Shop.BuyItem(instance.Value(ItemName));
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
