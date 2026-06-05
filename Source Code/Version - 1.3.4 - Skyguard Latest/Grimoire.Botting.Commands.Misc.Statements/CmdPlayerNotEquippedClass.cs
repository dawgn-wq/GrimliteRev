using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerNotEquippedClass : StatementCommand, IBotCommand
{
	public CmdPlayerNotEquippedClass()
	{
		base.Tag = "Player";
		base.Text = "Player's equipped class is not";
	}

	public Task Execute(IBotEngine instance)
	{
		if (Flash.Call<bool>("CheckPlayerClass", new string[2]
		{
			instance.Value(base.Value1),
			instance.Value(base.Value2)
		}))
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
		return "Player's equipped class is not: " + base.Value1 + ", " + base.Value2;
	}
}
