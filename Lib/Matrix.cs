using Lib.Drawing;

namespace Lib;

public class Matrix : AMatrix
{
    public override IDrawingStrategyCreator Creator => new DrawingStrategyCreator<DrawingStrategy>();

    public Matrix(uint rowCount, uint columnCount) : base(rowCount, columnCount)
    {
    }

    protected override IVector InitializeVector(uint size) => new Vector(size);

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
                    drawer.DrawElement(i, j, Matrix);
                }
            }
        }
    }
}
