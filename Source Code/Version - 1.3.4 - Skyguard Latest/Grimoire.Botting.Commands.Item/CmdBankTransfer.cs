using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Item;

public class CmdBankTransfer : IBotCommand
{
	public bool TransferFromBank { get; set; }

	public string ItemName { get; set; }

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Others;
		if (TransferFromBank)
		{
			await Player.Bank.TransferFromBank(instance.Value(ItemName));
		}
		else
		{
			await Player.Bank.TransferToBank(instance.Value(ItemName));
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return (TransferFromBank ? "Bank -> Inventory: " : "Inventory -> Bank: ") + ItemName;
	}
}
