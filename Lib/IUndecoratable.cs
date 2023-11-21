namespace Lib;

public interface IUndecoratable<out T>
{
	T GetOriginal();
}
