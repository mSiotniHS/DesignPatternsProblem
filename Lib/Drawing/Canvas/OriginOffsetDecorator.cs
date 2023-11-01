namespace Lib.Drawing.Canvas;

public class OriginOffsetDecorator : ICanvas
{
	private readonly ICanvas _canvas;
	private readonly Point _offset;

	public OriginOffsetDecorator(ICanvas canvas, Point offset)
	{
		_canvas = canvas;
		_offset = offset;
	}

	public void DrawLine(Point start, Point end) =>
		_canvas.DrawLine(AddOffset(start), AddOffset(end));

	public void DrawText(string value, double size, Point topLeft) =>
		_canvas.DrawText(value, size, AddOffset(topLeft));

	public Size MeasureTextSize(string text, double size) =>
		_canvas.MeasureTextSize(text, size);

	private Point AddOffset(Point point) =>
		new(_offset.X + point.X, _offset.Y + point.Y);
}
