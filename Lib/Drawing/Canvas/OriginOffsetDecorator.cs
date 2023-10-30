namespace Lib.Drawing.Canvas;

public class OriginOffsetDecorator : ICanvasAdapter
{
	private readonly ICanvasAdapter _adapter;
	private readonly Point _offset;

	public OriginOffsetDecorator(ICanvasAdapter adapter, Point offset)
	{
		_adapter = adapter;
		_offset = offset;
	}

	public void DrawLine(Point start, Point end) =>
		_adapter.DrawLine(AddOffset(start), AddOffset(end));

	public void DrawText(string value, double size, Point topLeft) =>
		_adapter.DrawText(value, size, AddOffset(topLeft));

	public Size MeasureTextSize(string text, double size) =>
		_adapter.MeasureTextSize(text, size);

	private Point AddOffset(Point point) =>
		new(_offset.X + point.X, _offset.Y + point.Y);
}
