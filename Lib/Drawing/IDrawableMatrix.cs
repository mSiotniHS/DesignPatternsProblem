namespace Lib.Drawing;

public interface IDrawableMatrix : IMatrix
{
    void Draw(IMatrixDrawer drawer);
}
