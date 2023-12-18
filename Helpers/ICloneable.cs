namespace Helpers;

public interface ICloneable<out T>
{
	T Clone();
}
