namespace Lib;

public interface IIterable<TCollection, out TElement>
{
    IIteratorFactory<TCollection, TElement> IteratorFactory { get; }
}
