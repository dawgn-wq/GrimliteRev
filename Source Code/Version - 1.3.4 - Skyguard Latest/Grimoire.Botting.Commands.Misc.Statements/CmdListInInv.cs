using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdListInInv : StatementCommand, IBotCommand
{
	public CmdListInInv()
	{
		base.Tag = "Item";
		base.Text = "All in Item List is in inventory";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Configuration.Instance.Items.TrueForAll((string x) => Player.Inventory.ContainsItem(x, "1")))
		{
			instance.Index++;
		}
		return Task.FromResult<object>(null);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "All in Item List is in inventory";
	}
}
