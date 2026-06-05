using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdIntGreaterThan : StatementCommand, IBotCommand
{
	public CmdIntGreaterThan()
	{
		base.Tag = "Misc";
		base.Text = "Int is greater than";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Configuration.Tempvalues[instance.Value(base.Value1)] < int.Parse(instance.Value(base.Value2)))
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
		return base.Value1 + " is greater than: " + base.Value2 + " (value)";
	}
}
