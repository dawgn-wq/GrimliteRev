using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdIndex : IBotCommand
{
	public enum IndexCommand
	{
		Up,
		Down,
		Goto
	}

	public IndexCommand Type { get; set; }

	public int Index { get; set; }

	public Task Execute(IBotEngine instance)
	{
		switch (Type)
		{
		case IndexCommand.Down:
		{
			int num2 = Index - 1;
			if (num2 > 0)
			{
				int num3 = (instance.Index += num2);
				if (num3 < instance.Configuration.Commands.Count)
				{
					instance.Index = num3;
				}
			}
			break;
		}
		case IndexCommand.Up:
		{
			int num4 = Index + 1;
			if (num4 > 0)
			{
				int num5 = (instance.Index -= num4);
				if (num5 > -1)
				{
					instance.Index = num5;
				}
			}
			break;
		}
		case IndexCommand.Goto:
		{
			int num = Index - 1;
			if (num > 0)
			{
				instance.Index = num;
			}
			break;
		}
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
		return Type switch
		{
			IndexCommand.Down => $"Index down: {Index}", 
			IndexCommand.Up => $"Index up: {Index}", 
			_ => $"Goto index: {Index}", 
		};
	}
}
