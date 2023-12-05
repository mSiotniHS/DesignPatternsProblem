using Helpers;
using Lib.Visitor;
using Lib.Visitor.Decorators;

namespace Lib;

public class MatrixHorizontalGroup : IMatrix
{
    private readonly List<IMatrix> _matrices;
    private readonly DefaultFirstCollection<(uint, uint), double> _emptySpace;

    public uint RowCount => _matrices.Select(matrix => matrix.RowCount).Max();
    public uint ColumnCount => _matrices.Select(matrix => matrix.ColumnCount).Aggregate(0u, Operators.Add);

    public MatrixHorizontalGroup(List<IMatrix> matrices)
    {
        _matrices = matrices;
        _emptySpace = new DefaultFirstCollection<(uint, uint), double>(0);
    }

    public double Get(uint row, uint column)
    {
        AssertCoordinates(row, column);

        var matrix = DetermineMatrix(row, column, out var left);
        if (matrix is not null)
        {
            return matrix.Get(row, column - left);
        }

        return _emptySpace.Get((row, column));
    }

    public void Set(uint row, uint column, double value)
    {
        AssertCoordinates(row, column);

        var matrix = DetermineMatrix(row, column, out var left);
        if (matrix is not null)
        {
            matrix.Set(row, column - left, value);
        }
        else
        {
            _emptySpace.Set((row, column), value);
        }
    }

    private void AssertCoordinates(uint row, uint column)
    {
        if (row >= RowCount || column >= ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(row), "Coordinates are too big");
        }
    }

    public void AcceptVisitor(IElementVisitor visitor)
    {
        var visitedMatrices = new List<IMatrix>();

        for (var row = 0u; row < RowCount; row++)
        {
            for (var column = 0u; column < ColumnCount; column++)
            {
                var matrix = DetermineMatrix(row, column, out var left);
                if (matrix is not null)
                {
                    if (visitedMatrices.Contains(matrix)) continue;

                    matrix.AcceptVisitor(new PlacementAlteringVisitor(visitor, FromMatrixToGroup(left)));
                    visitedMatrices.Add(matrix);
                }
                else
                {
                    visitor.VisitElement(row, column, _emptySpace.Get((row, column)));
                }
            }
        }
    }

    private static PlacementAlteringVisitor.CoordinateAlternator FromMatrixToGroup(uint left) =>
        (row, column) => (row, left + column);

    private IMatrix? DetermineMatrix(uint row, uint column, out uint left)
    {
        left = 0;

        foreach (var matrix in _matrices)
        {
            if (column < left + matrix.ColumnCount)
            {
                return row < matrix.RowCount ? matrix : null;
            }

            left += matrix.ColumnCount;
        }

        throw new ArgumentOutOfRangeException(nameof(column), "Wrong column");
    }

    public IMatrix GetOriginal() => this;

    public void AddMatrix(IMatrix matrix)
    {
        _matrices.Add(matrix);
    }
}
