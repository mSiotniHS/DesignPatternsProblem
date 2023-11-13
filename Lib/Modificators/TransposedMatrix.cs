namespace Lib.Modificators;

public class TransposedMatrix : IMatrix
{
    private readonly IMatrix _matrix;

    public uint RowCount => _matrix.ColumnCount;
    public uint ColumnCount => _matrix.RowCount;
    public IIteratorFactory<IMatrix, double> IteratorFactory => _matrix.IteratorFactory;

    public TransposedMatrix(IMatrix matrix)
    {
        _matrix = matrix;
    }

    public double Get(uint row, uint column) => _matrix.Get(column, row);
    public void Set(uint row, uint column, double value) => _matrix.Set(column, row, value);
}
