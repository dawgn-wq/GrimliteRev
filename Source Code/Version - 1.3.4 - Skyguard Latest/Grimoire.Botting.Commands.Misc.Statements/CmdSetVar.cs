using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdSetVar : StatementCommand, IBotCommand
{
	public CmdSetVar()
	{
		base.Tag = "Misc";
		base.Text = "Set Variable";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Configuration.Tempvariable.ContainsKey(base.Value1))
		{
			Configuration.Tempvariable.Add(base.Value1, base.Value2);
		}
		else
		{
			Configuration.Tempvariable[base.Value1] = base.Value2;
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
		return "Set Variable " + base.Value1 + ": " + base.Value2;
	}
}
