using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers;

public class HandlerAura : IJsonMessageHandler
{
	public string[] HandledCommands { get; } = new string[1] { "ct" };

	public async void Handle(JsonMessage message)
	{
		await Task.Delay(10);
		JToken jToken = message.DataObject?["a"];
		if (jToken == null)
		{
			return;
		}
		foreach (JToken item in (IEnumerable<JToken>)jToken)
		{
			bool onEnemy = item["tInf"].ToString().Split(':')[0] == "m";
			int entityID = int.Parse(item["tInf"].ToString().Split(':')[1]);
			JToken jToken2 = item["cmd"];
			switch (jToken2.ToString())
			{
			case "aura+":
			case "aura++":
			case "aura+p":
				addAura(item["auras"], onEnemy, entityID);
				break;
			case "aura-":
			case "aura--":
				removeAura(item["aura"], onEnemy, entityID);
				break;
			}
		}
	}

	void IJsonMessageHandler.Handle(JsonMessage message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Handle
		this.Handle(message);
	}

	public async void addAura(JToken datas, bool onEnemy, int entityID)
	{
		if (datas == null)
		{
			return;
		}
		foreach (JToken data in (IEnumerable<JToken>)datas)
		{
			Aura aura = ((!onEnemy) ? World.PlayerAuras.FirstOrDefault((Aura x) => x.Name.Equals(data["nam"].ToString()) && x.entityID.Equals(entityID)) : World.EnemyAuras.FirstOrDefault((Aura x) => x.Name.Equals(data["nam"].ToString()) && x.entityID.Equals(entityID)));
			if (!onEnemy)
			{
				if (aura == null)
				{
					Aura aura2 = new Aura
					{
						entityID = entityID,
						Name = data["nam"].ToString(),
						Value = 1,
						realValue = ((data["val"] == null) ? null : data["val"].ToString()),
						disableType = ((data["cat"] == null) ? null : data["cat"].ToString()),
						durationSpan = int.Parse(data["dur"].ToString()),
						timeNow = DateTime.Now
					};
					World.PlayerAuras.Add(aura2);
					World.auraCountdown += aura2.countdown;
					continue;
				}
				World.auraCountdown -= aura.countdown;
				World.PlayerAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.Value++;
				});
				World.PlayerAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.realValue = ((data["val"] == null) ? null : data["val"].ToString());
				});
				World.PlayerAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.disableType = ((data["cat"] == null) ? null : data["cat"].ToString());
				});
				World.PlayerAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.durationSpan = int.Parse(data["dur"].ToString());
				});
				World.PlayerAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.timeNow = DateTime.Now;
				});
				World.auraCountdown += aura.countdown;
			}
			else if (aura == null)
			{
				Aura aura3 = new Aura
				{
					entityID = entityID,
					Name = data["nam"].ToString(),
					Value = 1,
					realValue = ((data["val"] == null) ? null : data["val"].ToString()),
					disableType = ((data["cat"] == null) ? null : data["cat"].ToString()),
					durationSpan = int.Parse(data["dur"].ToString()),
					timeNow = DateTime.Now
				};
				World.EnemyAuras.Add(aura3);
				World.auraCountdown += aura3.countdown;
			}
			else
			{
				World.auraCountdown -= aura.countdown;
				World.EnemyAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.Value++;
				});
				World.EnemyAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.realValue = ((data["val"] == null) ? null : data["val"].ToString());
				});
				World.EnemyAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.disableType = ((data["cat"] == null) ? null : data["cat"].ToString());
				});
				World.EnemyAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.durationSpan = int.Parse(data["dur"].ToString());
				});
				World.EnemyAuras.Where((Aura a) => a.Name == data["nam"].ToString()).ToList().ForEach(delegate(Aura v)
				{
					v.timeNow = DateTime.Now;
				});
				World.auraCountdown += aura.countdown;
			}
		}
	}

	public void removeAura(JToken datas, bool onEnemy, int entityID)
	{
		if (datas != null)
		{
			Aura aura = ((!onEnemy) ? World.PlayerAuras.FirstOrDefault((Aura x) => x.Name.Equals(datas["nam"].ToString()) && x.entityID.Equals(entityID)) : World.EnemyAuras.FirstOrDefault((Aura x) => x.Name.Equals(datas["nam"].ToString()) && x.entityID.Equals(entityID)));
			if (!onEnemy)
			{
				World.PlayerAuras.Remove(aura);
			}
			else
			{
				World.EnemyAuras.Remove(aura);
			}
			World.auraCountdown -= aura.countdown;
		}
	}
}
