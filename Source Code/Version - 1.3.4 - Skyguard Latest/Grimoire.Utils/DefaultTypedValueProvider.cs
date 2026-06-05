using System;

namespace Grimoire.Utils;

[Serializable]
public class DefaultTypedValueProvider : TypedValueProvider
{
	public object Provide(Type type)
	{
		try
		{
			return Activator.CreateInstance(type);
		}
		catch
		{
			return type.GetDefaultValue();
		}
	}

	object TypedValueProvider.Provide(Type type)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Provide
		return this.Provide(type);
	}
}
