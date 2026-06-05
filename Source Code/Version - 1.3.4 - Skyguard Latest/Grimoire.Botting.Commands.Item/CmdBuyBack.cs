using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Tools.Buyback;

namespace Grimoire.Botting.Commands.Item;

public class CmdBuyBack : IBotCommand
{
	public string ItemName { get; set; }

	public int PageNumberCap { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Transaction;
		if (Player.Inventory.ContainsItem(ItemName))
		{
			return;
		}
		try
		{
			await Task.Run(async delegate
			{
				using AutoBuyBack abb = new AutoBuyBack();
				await abb.Perform(ItemName, PageNumberCap);
			});
			Player.Logout();
		}
		catch
		{
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Buy back: " + ItemName;
	}
}
