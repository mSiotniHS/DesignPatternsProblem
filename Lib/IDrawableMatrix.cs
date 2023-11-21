using Lib.Drawing;

namespace Lib;

public interface IDrawableMatrix : IMatrix
{
    public IDrawingStrategyCreator Creator { get; }
    void Draw(IMatrixDrawer drawer);
    IDrawableMatrix GetComponent();
}
