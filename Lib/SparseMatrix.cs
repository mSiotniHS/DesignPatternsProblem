using Lib.Drawing;

namespace Lib;

public class SparseMatrix : AMatrix
{
    public SparseMatrix(uint rowCount, uint columnCount) : base(rowCount, columnCount)
    {
    }

    protected override IVector InitializeVector(uint size) => new SparseVector(size);

    public override void Draw(IMatrixDrawer drawer)
    {
        drawer.DrawBraces(this);

        for (var i = 0u; i < RowCount; i++)
        {
            for (var j = 0u; j < ColumnCount; j++)
            {
                var value = Get(i, j);
                if (value != 0)
                {
                    drawer.DrawElement(i, j, this);
                }
            }
        }
    }
}
