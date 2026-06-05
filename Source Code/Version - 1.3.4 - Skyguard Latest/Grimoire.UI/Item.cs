namespace Grimoire.UI;

public class Item : ISetInterface
{
	public string Type { get; set; }

	public string Name { get; set; }

	public override string ToString()
	{
		return "[" + Type + "] : " + Name;
	}
}
