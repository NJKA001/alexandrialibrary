using System;
using System.Collections.Generic;

namespace Alexandria.Metadata
{
	public interface IElement<T> : IEquatable<T>
	{
		Uri Identifier { get; }
		T Value { get; }
	}
}