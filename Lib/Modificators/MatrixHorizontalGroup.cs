using Lib.Drawing;
using Lib.Helpers;

namespace Lib.Modificators;

public class MatrixHorizontalGroup : IMatrix
{
    private readonly List<IMatrix> _matrices;

    public uint RowCount => _matrices.Select(matrix => matrix.RowCount).Max();
    public uint ColumnCount => _matrices.Select(matrix => matrix.ColumnCount).Aggregate(0u, Operators.Add);

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

    public void AcceptVisitor(IElementVisitor visitor)
    {
        throw new NotImplementedException();
    }

    public IMatrix GetOriginal() => this;

    public void AddMatrix(IMatrix matrix)
    {
        _matrices.Add(matrix);
    }
}
