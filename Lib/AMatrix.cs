namespace Lib;

public abstract class AMatrix : ADrawableMatrix
{
    private readonly IVector[] _rows;

    public override uint RowCount => (uint)_rows.Length;
    public override uint ColumnCount => _rows[0].Dimension;


    protected AMatrix(uint rowCount, uint columnCount)
    {
        _rows = new IVector[rowCount];

        for (var i = 0; i < _rows.Length; i++)
        {
            _rows[i] = InitializeVector(columnCount);
        }
    }

    protected abstract IVector InitializeVector(uint size);

    public override double Get(uint row, uint column) =>
        _rows[row].Get(column);

    public override void Set(uint row, uint column, double value) =>
        _rows[row].Set(column, value);
}
