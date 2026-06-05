using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Map;

public class CmdWalk : IBotCommand
{
	public string X { get; set; }

	public string Y { get; set; }

	public string Type { get; set; }

	private string _x
	{
		get
		{
			if (!(Type == "Random"))
			{
				return BotManager.Instance.ActiveBotEngine.Value(X);
			}
			return new Random().Next(150, 700).ToString();
		}
	}

	private string _y
	{
		get
		{
			if (!(Type == "Random"))
			{
				return BotManager.Instance.ActiveBotEngine.Value(Y);
			}
			return new Random().Next(320, 450).ToString();
		}
	}

	public async Task Execute(IBotEngine instance)
	{
		BotData.BotState = BotData.State.Others;
		Player.WalkToPoint(_x, _y);
		await instance.WaitUntil(delegate
		{
			float[] position = Player.Position;
			return position[0].ToString() == X && position[1].ToString() == Y;
		}, null, 6, 500);
	}

	Task IBotCommand.Execute(IBotEngine instance)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Execute
		return this.Execute(instance);
	}

	public override string ToString()
	{
		if (!(Type == "Random"))
		{
			return "Walk to: " + X + ", " + Y;
		}
		return "Walk randomly";
	}
}
