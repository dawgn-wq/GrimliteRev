using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Misc.Statements;

public class CmdQuestAvailable : StatementCommand, IBotCommand
{
	public CmdQuestAvailable()
	{
		base.Tag = "Quest";
		base.Text = "Quest is available";
	}

	public async Task Execute(IBotEngine instance)
	{
		if (Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == int.Parse(instance.Value(base.Value1))) == null)
		{
			Player.Quests.GetQuest(int.Parse(instance.Value(base.Value1)));
			await instance.WaitUntil(() => Player.Quests.LoadedQuests.Find((Grimoire.Game.Data.Quest q) => q.Id == int.Parse(instance.Value(base.Value1))) != null, null, 6, 500);
		}
		if (!Player.Quests.IsAvailable(int.Parse(instance.Value(base.Value1))))
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
		return "Quest is available: " + base.Value1;
	}
}
