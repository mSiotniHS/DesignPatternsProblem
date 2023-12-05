using Lib.Visitor;
using Lib.Visitor.Decorators;

namespace Lib.Decorators;

public class TransposedMatrix : IMatrix
{
    private readonly IMatrix _matrix;

    public uint RowCount => _matrix.ColumnCount;
    public uint ColumnCount => _matrix.RowCount;

    public TransposedMatrix(IMatrix matrix)
    {
        _matrix = matrix;
    }

    public double Get(uint row, uint column)
    {
        var (alteredRow, alteredColumn) = ModifyCoordinates(row, column);
        return _matrix.Get(alteredRow, alteredColumn);
    }

    public void Set(uint row, uint column, double value)
    {
        var (alteredRow, alteredColumn) = ModifyCoordinates(row, column);
        _matrix.Set(alteredRow, alteredColumn, value);
    }

    public void AcceptVisitor(IElementVisitor visitor) =>
        _matrix.AcceptVisitor(new PlacementAlteringVisitor(visitor, ModifyCoordinates));

    private static (uint, uint) ModifyCoordinates(uint row, uint column) =>
        (column, row);

    public IMatrix GetOriginal() => _matrix;
}
