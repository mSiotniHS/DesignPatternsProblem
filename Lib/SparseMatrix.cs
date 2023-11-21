using Lib.Drawing;

namespace Lib;

public class SparseMatrix : AMatrix
{
    public override IDrawingStrategyCreator Creator => new DrawingStrategyCreator<DrawingStrategy>();

    public SparseMatrix(uint rowCount, uint columnCount) : base(rowCount, columnCount)
    {
    }

    protected override IVector InitializeVector(uint size) => new SparseVector(size);

    public override IDrawableMatrix GetComponent() => this;

    private class DrawingStrategy : IDrawingStrategy
    {
        public IReadOnlyMatrix Matrix { get; init; }

        public void Draw(IMatrixDrawer drawer)
        {
            drawer.DrawBraces(Matrix);

            for (var i = 0u; i < Matrix.RowCount; i++)
            {
                for (var j = 0u; j < Matrix.ColumnCount; j++)
                {
                    var value = Matrix.Get(i, j);
                    if (value != 0)
                    {
                        drawer.DrawElement(i, j, Matrix);
                    }
                }
            }
        }
    }
}
