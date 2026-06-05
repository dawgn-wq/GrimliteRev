using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdItemNotPickupable : StatementCommand, IBotCommand
{
	public CmdItemNotPickupable()
	{
		base.Tag = "Item";
		base.Text = "Has not dropped";
	}

	public Task Execute(IBotEngine instance)
	{
		if (World.DropStack.Contains(instance.Value(base.Value1)))
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
		return "Item has not dropped: " + base.Value1;
	}
}
