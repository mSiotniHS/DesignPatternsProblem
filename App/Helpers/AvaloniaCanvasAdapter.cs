using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Lib.Drawing.Canvas;

namespace App.Helpers;

public class AvaloniaCanvasAdapter : ICanvasAdapter
{
	private readonly Canvas _canvas;

	public AvaloniaCanvasAdapter(Canvas canvas)
	{
		_canvas = canvas;
	}

	public void DrawLine(Point start, Point end)
	{
		_canvas.Children.Add(new Line
		{
			StartPoint = new Avalonia.Point(start.X, start.Y),
			EndPoint = new Avalonia.Point(end.X, end.Y),
			Stroke = new SolidColorBrush(Colors.White)
		});
	}

	public void DrawText(string value, double size, Point topLeft)
	{
		var text = new TextBlock { Text = value, FontSize = size };
		text.SetValue(Canvas.LeftProperty, topLeft.X);
		text.SetValue(Canvas.TopProperty, topLeft.Y);

		_canvas.Children.Add(text);
	}

	public Size MeasureTextSize(string text, double size)
	{
		var formattedText = new FormattedText(
			text,
			CultureInfo.CurrentCulture,
			FlowDirection.LeftToRight,
			Typeface.Default,
			size,
			null
		);

		return new Size(formattedText.Width, formattedText.Height);
	}
}
