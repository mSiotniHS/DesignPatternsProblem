namespace Lib;

public interface IReadOnlyMatrix : IIterableMatrix
{
    public double Get(uint row, uint column);
    public uint RowCount { get; }
    public uint ColumnCount { get; }
}

public interface IIterableMatrix : IIterable<IMatrix, double>
{
}
