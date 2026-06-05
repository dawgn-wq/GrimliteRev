using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc;

public class CmdInt : IBotCommand
{
	public enum Types
	{
		Set,
		Upper,
		Lower
	}

	public Types type { get; set; }

	public string Int { get; set; }

	public int Value { get; set; }

	public Task Execute(IBotEngine instance)
	{
		switch (type)
		{
		case Types.Set:
			if (Configuration.Tempvalues.ContainsKey(Int))
			{
				Configuration.Tempvalues[Int] = Value;
			}
			else
			{
				Configuration.Tempvalues.Add(Int, Value);
			}
			break;
		case Types.Upper:
			Configuration.Tempvalues[Int]++;
			break;
		case Types.Lower:
			Configuration.Tempvalues[Int]--;
			break;
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
		return type switch
		{
			Types.Set => $"Set {Int}: {Value}", 
			Types.Upper => "Increase " + Int + " by 1", 
			_ => "Decrease " + Int + " by 1", 
		};
	}
}
