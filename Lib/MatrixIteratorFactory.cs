namespace Lib;

public class MatrixIteratorFactory<T> : IIteratorFactory<IMatrix, double>
    where T : class, IIterator<IMatrix, double>, new()
{
    public IIterator<IMatrix, double> CreateIterator(IMatrix matrix)
    {
        var iterator = new T
        {
            Collection = matrix
        };
        return iterator;
    }
}