using System;

namespace Grimoire.Botting;

public interface IBotEngine
{
	bool IsRunning { get; set; }

	int Index { get; set; }

	Configuration Configuration { get; set; }

	int CurrentConfiguration { get; set; }

	event Action<bool> IsRunningChanged;

	event Action<int> IndexChanged;

	event Action<Configuration> ConfigurationChanged;

	bool IsVar(string value);

	string GetVar(string value);

	string Value(string var);

	void Start(Configuration config);

	void Stop();

	void Resume(Configuration config);

	void Pause();
}
