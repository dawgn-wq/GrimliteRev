using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Item;

public class CmdBankSwap : IBotCommand
{
	public string BankItemName { get; set; }

	public string InventoryItemName { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Others;
		Player.Bank.Swap(instance.Value(InventoryItemName), instance.Value(BankItemName));
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Bank <-> Inventory: " + BankItemName + "<->" + InventoryItemName;
	}
}
