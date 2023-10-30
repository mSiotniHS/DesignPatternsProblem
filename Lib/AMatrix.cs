using Lib.Drawing;

namespace Lib;

public abstract class AMatrix : IDrawableMatrix
{
    private readonly IVector[] _rows;

    protected AMatrix(uint rowCount, uint columnCount)
    {
        _rows = new IVector[rowCount];

        for (var i = 0; i < _rows.Length; i++)
        {
            _rows[i] = InitializeVector(columnCount);
        }
    }

    protected abstract IVector InitializeVector(uint size);

    public double Get(uint row, uint column) =>
        _rows[row].Get(column);

    public void Set(uint row, uint column, double value) =>
        _rows[row].Set(column, value);

    public uint RowCount => (uint) _rows.Length;
    public uint ColumnCount => _rows[0].Dimension;

    public abstract void Draw(IMatrixDrawer drawer);
}
