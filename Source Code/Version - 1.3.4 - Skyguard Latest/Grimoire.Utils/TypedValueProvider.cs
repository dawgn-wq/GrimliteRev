using System;

namespace Grimoire.Utils;

public interface TypedValueProvider
{
	object Provide(Type type);
}
