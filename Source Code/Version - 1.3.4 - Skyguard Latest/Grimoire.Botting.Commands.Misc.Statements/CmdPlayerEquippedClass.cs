using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdPlayerEquippedClass : StatementCommand, IBotCommand
{
	public CmdPlayerEquippedClass()
	{
		base.Tag = "Player";
		base.Text = "Player's equipped class is";
	}

	public Task Execute(IBotEngine instance)
	{
		if (!Flash.Call<bool>("CheckPlayerClass", new string[2]
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
		return "Player's equipped class is: " + base.Value1 + ", " + base.Value2;
	}
}
