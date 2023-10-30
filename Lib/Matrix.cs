using Lib.Drawing;

namespace Lib;

public class Matrix : AMatrix
{
    public Matrix(uint rowCount, uint columnCount) : base(rowCount, columnCount)
    {
    }

    protected override IVector InitializeVector(uint size) => new Vector(size);

    public override void Draw(IMatrixDrawer drawer)
    {
        drawer.DrawBraces(this);

        for (var i = 0u; i < RowCount; i++)
        {
            for (var j = 0u; j < ColumnCount; j++)
            {
                drawer.DrawElement(i, j, this);
            }
        }
    }
}
