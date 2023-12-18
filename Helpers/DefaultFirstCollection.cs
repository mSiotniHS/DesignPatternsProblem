namespace Helpers;

public class DefaultFirstCollection<TKey, TValue> : ICloneable<DefaultFirstCollection<TKey, TValue>>
	where TKey : notnull
	where TValue : IEquatable<TValue>
{
	private readonly Dictionary<TKey, TValue> _nonDefaultValues;
	private readonly TValue _defaultValue;

	public DefaultFirstCollection(TValue defaultValue)
	{
		_nonDefaultValues = new Dictionary<TKey, TValue>();
		_defaultValue = defaultValue;
	}

	public TValue Get(TKey key) =>
		_nonDefaultValues.GetValueOrDefault(key, _defaultValue);

	public void Set(TKey key, TValue value)
	{
		if (!value.Equals(_defaultValue))
		{
			_nonDefaultValues[key] = value;
		}
		else if (value.Equals(_defaultValue) && _nonDefaultValues.ContainsKey(key))
		{
			_nonDefaultValues.Remove(key);
		}
	}

	public DefaultFirstCollection<TKey, TValue> Clone()
	{
		var clone = new DefaultFirstCollection<TKey, TValue>(_defaultValue);
		foreach (var (key, value) in _nonDefaultValues)
		{
			clone.Set(key, value);
		}

		return clone;
	}
}
