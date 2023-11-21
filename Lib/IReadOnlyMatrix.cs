namespace Lib;

public interface IReadOnlyMatrix
{
    public double Get(uint row, uint column);
    public uint RowCount { get; }
    public uint ColumnCount { get; }
}
