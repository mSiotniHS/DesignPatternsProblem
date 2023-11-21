namespace Lib.Modificators;

public class TransposedMatrix : ADrawableMatrix
{
    private readonly IDrawableMatrix _matrix;

    public override uint RowCount => _matrix.ColumnCount;
    public override uint ColumnCount => _matrix.RowCount;
    public override IDrawingStrategyCreator Creator => _matrix.Creator;

    public TransposedMatrix(IDrawableMatrix matrix)
    {
        _matrix = matrix;
    }

    public override double Get(uint row, uint column) => _matrix.Get(column, row);
    public override void Set(uint row, uint column, double value) => _matrix.Set(column, row, value);

    public override IDrawableMatrix GetComponent() => _matrix.GetComponent();
}
