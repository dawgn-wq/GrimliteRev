using System;
using System.Collections.Generic;

namespace Grimoire.Utils;

[Serializable]
public class EmptyListProvider<T> : TypedValueProvider
{
	public object Provide(Type type)
	{
		return new List<T>();
	}

	object TypedValueProvider.Provide(Type type)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Provide
		return this.Provide(type);
	}
}
