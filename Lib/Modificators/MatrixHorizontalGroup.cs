using Lib.Helpers;

namespace Lib.Modificators;

public class MatrixHorizontalGroup : IMatrix
{
    private readonly List<IMatrix> _matrices;

    public uint RowCount => _matrices.Select(matrix => matrix.RowCount).Max();
    public uint ColumnCount => _matrices.Select(matrix => matrix.ColumnCount).Aggregate(0u, Operators.Add);
    public IIteratorFactory<IMatrix, double> IteratorFactory => new MatrixIteratorFactory<Iterator>();

    public MatrixHorizontalGroup(List<IMatrix> matrices)
    {
        _matrices = matrices;
    }

    public double Get(uint row, uint column)
    {
        throw new NotImplementedException();
    }

    public void Set(uint row, uint column, double value)
    {
        throw new NotImplementedException();
    }

    public void AddMatrix(IMatrix matrix)
    {
        _matrices.Add(matrix);
    }

    private class Iterator : IIterator<IMatrix, double>
    {
        public IMatrix Collection { get; init; }

        public double GetNext()
        {
            var actualCollection = (MatrixHorizontalGroup) Collection;
        }

        public bool HasNext()
        {
            throw new NotImplementedException();
        }
    }
}
