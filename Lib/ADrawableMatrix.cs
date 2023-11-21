using Lib.Drawing;

namespace Lib;

public abstract class ADrawableMatrix : IDrawableMatrix
{
    public abstract uint RowCount { get; }
    public abstract uint ColumnCount { get; }
    public abstract double Get(uint row, uint column);
    public abstract void Set(uint row, uint column, double value);

    public abstract IDrawingStrategyCreator Creator { get; }

    public void Draw(IMatrixDrawer drawer)
    {
        var strategy = Creator.CreateStrategy(this);
        strategy.Draw(drawer);
    }

    public abstract IDrawableMatrix GetComponent();
}
