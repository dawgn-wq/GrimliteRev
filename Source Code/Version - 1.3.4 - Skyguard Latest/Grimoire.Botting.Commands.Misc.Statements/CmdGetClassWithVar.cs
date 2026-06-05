using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdGetClassWithVar : StatementCommand, IBotCommand
{
	public CmdGetClassWithVar()
	{
		base.Tag = "This player";
		base.Text = "Get equipped class with Variable";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Configuration.Tempvariable.ContainsKey(base.Value1))
		{
			Configuration.Tempvariable.Add(base.Value1, Player.Class);
		}
		else
		{
			Configuration.Tempvariable[base.Value1] = Player.Class;
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
		return "Get equipped class with Variable: " + base.Value1;
	}
}
