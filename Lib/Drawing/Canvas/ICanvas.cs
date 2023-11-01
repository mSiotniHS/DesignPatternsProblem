using Lib.Drawing.Common;

namespace Lib.Drawing.Canvas;

public interface ICanvas
{
    void DrawLine(Point start, Point end);
    void DrawText(string value, double size, Point topLeft);
    Size MeasureTextSize(string text, double size);
}
