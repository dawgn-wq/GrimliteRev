using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game.Data;

public class ItemBase
{
	[JsonProperty("ItemID")]
	public virtual int ID { get; set; }

	[JsonProperty("sName")]
	[JsonConverter(typeof(TrimConverter))]
	public virtual string Name { get; set; }

	[JsonProperty("sDesc")]
	public virtual string Description { get; set; }

	[JsonProperty("iQty")]
	public virtual int Quantity { get; set; }

	[JsonProperty("iStk")]
	public virtual int MaxStack { get; set; }

	[JsonProperty("bUpg")]
	[JsonConverter(typeof(StringBoolConverter))]
	public virtual bool Upgrade { get; set; }

	[JsonProperty("bCoins")]
	[JsonConverter(typeof(StringBoolConverter))]
	public virtual bool Coins { get; set; }

	[JsonProperty("sType")]
	public virtual string Category { get; set; }

	[JsonProperty("bTemp")]
	[JsonConverter(typeof(StringBoolConverter))]
	public virtual bool Temp { get; set; }

	[JsonProperty("sFile")]
	public virtual string File { get; set; }

	[JsonProperty("sLink")]
	public virtual string Link { get; set; }

	public override string ToString()
	{
		return $"{Name} [{ID}] x {Quantity}";
	}
}
