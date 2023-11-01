namespace Lib.Drawing.Canvas;

public record struct Point(double X, double Y);
public record struct Size(double Width, double Height);

public interface ICanvas
{
    void DrawLine(Point start, Point end);
    void DrawText(string value, double size, Point topLeft);
    Size MeasureTextSize(string text, double size);
}
