using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdUpdateVar : StatementCommand, IBotCommand
{
	public CmdUpdateVar()
	{
		base.Tag = "Misc";
		base.Text = "Update Variable";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Configuration.Tempvariable.ContainsKey(base.Value1))
		{
			Configuration.Tempvariable[base.Value1] = base.Value2;
		}
		else
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
		return "Update Variable " + base.Value1 + ": " + base.Value2;
	}
}
