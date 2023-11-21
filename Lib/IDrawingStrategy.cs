using Lib.Drawing;

namespace Lib;

public interface IDrawingStrategy
{
    IReadOnlyMatrix Matrix { get; init; }
    void Draw(IMatrixDrawer drawer);
}
