using Lib.Drawing;

namespace Lib;

public class SparseMatrix : AMatrix
{
    public SparseMatrix(uint rowCount, uint columnCount) : base(rowCount, columnCount)
    {
    }

    protected override IVector InitializeVector(uint size) => new SparseVector(size);
}

public class SparseMatrixDrawingStrategy : IDrawingStrategy<IMatrixDrawer>
{
    private readonly IReadOnlyMatrix _matrix;

    public SparseMatrixDrawingStrategy(IReadOnlyMatrix matrix)
    {
        _matrix = matrix;
    }

    public void Draw(IMatrixDrawer drawer)
    {
        drawer.DrawBraces(_matrix);

        for (var i = 0u; i < _matrix.RowCount; i++)
        {
            for (var j = 0u; j < _matrix.ColumnCount; j++)
            {
                var value = _matrix.Get(i, j);
                if (value != 0)
                {
                    drawer.DrawElement(i, j, _matrix);
                }
            }
        }
    }
}
