namespace Lib.Drawing;

public interface IMatrixDrawer
{
    void DrawBraces(IReadOnlyMatrix matrix);
    void DrawElement(uint row, uint column, IReadOnlyMatrix matrix);
}
