using Lib.Visitor;
using Lib.Visitor.Decorators;

namespace Lib.Decorators;

public class SwappedMatrix : IMatrix
{
    private readonly IMatrix _matrix;
    private readonly uint[] _rowsPermutation;
    private readonly uint[] _columnsPermutation;

    public uint RowCount => _matrix.RowCount;
    public uint ColumnCount => _matrix.ColumnCount;

    public SwappedMatrix(IMatrix matrix, uint swappedRowCount, uint swappedColumnCount)
    {
        _matrix = matrix;

        _rowsPermutation = GeneratePermutation(matrix.RowCount, swappedRowCount);
        _columnsPermutation = GeneratePermutation(matrix.ColumnCount, swappedColumnCount);
    }

    public double Get(uint row, uint column)
    {
        var actualRow = GetActualRow(row);
        var actualColumn = GetActualColumn(column);

        return _matrix.Get(actualRow, actualColumn);
    }

    public void Set(uint row, uint column, double value)
    {
        var actualRow = GetActualRow(row);
        var actualColumn = GetActualColumn(column);

        _matrix.Set(actualRow, actualColumn, value);
    }

    public void AcceptVisitor(IElementVisitor visitor)
    {
        _matrix.AcceptVisitor(new ElementAlteringVisitor(visitor, Get));
    }

    public IMatrix GetOriginal() => _matrix;

    private static uint[] GeneratePermutation(uint elementCount, uint permutedElementCount) =>
        Enumerable
            .Range(0, (int) elementCount)
            .OrderBy(_ => Random.Shared.Next())
            .Take((int) permutedElementCount)
            .Select(el => (uint) el)
            .ToArray();

    private uint GetActualRow(uint row)
    {
        var elIdx = Array.IndexOf(_rowsPermutation, row);
        if (elIdx == -1)
        {
            return row;
        }

        return elIdx == _rowsPermutation.Length - 1 ? _rowsPermutation[0] : _rowsPermutation[elIdx + 1];
    }

    private uint GetActualColumn(uint column)
    {
        var elIdx = Array.IndexOf(_columnsPermutation, column);
        if (elIdx == -1)
        {
            return column;
        }

        return elIdx == _columnsPermutation.Length - 1 ? _columnsPermutation[0] : _columnsPermutation[elIdx + 1];
    }
}
