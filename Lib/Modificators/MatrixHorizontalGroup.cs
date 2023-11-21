using Lib.Drawing;
using Lib.Helpers;

namespace Lib.Modificators;

public class MatrixHorizontalGroup : ADrawableMatrix
{
    private readonly List<IDrawableMatrix> _matrices;

    public override uint RowCount => _matrices.Select(matrix => matrix.RowCount).Max();
    public override uint ColumnCount => _matrices.Select(matrix => matrix.ColumnCount).Aggregate(0u, Operators.Add);
    public override IDrawingStrategyCreator Creator => new DrawingStrategyCreator<DrawingStrategy>();
    public override IDrawableMatrix GetComponent() => this;

    public MatrixHorizontalGroup(List<IDrawableMatrix> matrices)
    {
        _matrices = matrices;
    }

    public override double Get(uint row, uint column)
    {
        throw new NotImplementedException();
    }

    public override void Set(uint row, uint column, double value)
    {
        throw new NotImplementedException();
    }

    public void AddMatrix(IDrawableMatrix matrix)
    {
        _matrices.Add(matrix);
    }

    private class DrawingStrategy : IDrawingStrategy
    {
        public IReadOnlyMatrix Matrix { get; init; }

        public void Draw(IMatrixDrawer drawer)
        {
            var actualMatrix = (MatrixHorizontalGroup) Matrix;

            foreach (var matrix in actualMatrix._matrices)
            {
                matrix.Draw(drawer);  // TODO add смещение
            }
        }
    }
}
