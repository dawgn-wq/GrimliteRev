using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdIntEqualInt : StatementCommand, IBotCommand
{
	public CmdIntEqualInt()
	{
		base.Tag = "Misc";
		base.Text = "Int is equal to Int";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Configuration.Tempvalues[instance.Value(base.Value1)] != Configuration.Tempvalues[instance.Value(base.Value2)])
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
		return base.Value1 + " is equal to: " + base.Value2 + " (int)";
	}
}
