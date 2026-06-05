using System.Threading.Tasks;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdMonsterHealthGreaterThan : StatementCommand, IBotCommand
{
	public CmdMonsterHealthGreaterThan()
	{
		base.Tag = "Monster";
		base.Text = "Monster's health is greater than";
	}

	public async Task Execute(IBotEngine instance)
	{
		if (World.MonsterHealth(instance.Value(base.Value1)) <= int.Parse(instance.Value(base.Value2)))
		{
			instance.Index++;
		}
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		return "Monster's health is greater than: " + base.Value1 + ", " + base.Value2;
	}
}
