namespace Lib;

public interface IIteratorFactory<TCollection, out TElement>
{
    IIterator<TCollection, TElement> CreateIterator(TCollection collection);
}