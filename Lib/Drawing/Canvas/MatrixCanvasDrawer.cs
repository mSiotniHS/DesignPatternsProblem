using System.Globalization;
using Lib.Drawing.Common;

namespace Lib.Drawing.Canvas;

public class MatrixCanvasDrawer : IMatrixDrawer
{
    private const double FontSize = 16;
    private const double ElementGap = 10;
    private const double AfterDotMaxLength = 3;
    private const double BorderGap = 10;

    private readonly ICanvas _canvas;

    private double[]? _columnWidths;
    private double? _rowHeight;

    public MatrixCanvasDrawer(ICanvas canvas)
    {
        _canvas = canvas;
        _columnWidths = null;
        _rowHeight = null;
    }

    public void DrawBraces(IReadOnlyMatrix matrix)
    {
        _columnWidths ??= CalculateColumnWidths(matrix);
        _rowHeight ??= CalculateRowHeight(matrix);

        var braceHeight =
            BorderGap * 2  // отступ от рамки
            + _rowHeight.Value * matrix.RowCount  // высоты элементов
            + ElementGap * (matrix.RowCount - 1); // расстояния между элементами

        _canvas.DrawLine(new Point(0, 0), new Point(ElementGap, 0));
        _canvas.DrawLine(new Point(0, 0), new Point(0, braceHeight));
        _canvas.DrawLine(new Point(0, braceHeight), new Point(ElementGap, braceHeight));

        var rightBraceX =
            _columnWidths.Aggregate(0.0, (sum, next) => sum + next)  // ширины столбцов
            + (matrix.ColumnCount - 1) * ElementGap  // расстояние между элементами
            + 2 * BorderGap;  // отступы от границ

        _canvas.DrawLine(new Point(rightBraceX - ElementGap, 0), new Point(rightBraceX, 0));
        _canvas.DrawLine(new Point(rightBraceX, 0), new Point(rightBraceX, braceHeight));
        _canvas.DrawLine(new Point(rightBraceX - ElementGap, braceHeight), new Point(rightBraceX, braceHeight));
    }

    public void DrawElement(uint row, uint column, IReadOnlyMatrix matrix)
    {
        _columnWidths ??= CalculateColumnWidths(matrix);
        _rowHeight ??= CalculateRowHeight(matrix);

        var someColumnWidthsSum = _columnWidths
            .Take((int) column)
            .Aggregate(0.0, (sum, next) => sum + next);
        var startingX =
            someColumnWidthsSum
            + BorderGap
            + column * ElementGap;
        var startingY =
            _rowHeight.Value * row
            + BorderGap
            + row * ElementGap;

        _canvas.DrawText(
            FormatNumber(matrix.Get(row, column)),
            FontSize,
            new Point(startingX, startingY));
    }

    private double[] CalculateColumnWidths(IReadOnlyMatrix matrix)
    {
        var widths = new double[matrix.ColumnCount];

        for (var column = 0u; column < matrix.ColumnCount; column++)
        {
            var maxLength = 0.0;

            for (var row = 0u; row < matrix.RowCount; row++)
            {
                var length = GetLength(matrix.Get(row, column));
                if (length > maxLength)
                {
                    maxLength = length;
                }
            }

            widths[column] = maxLength;
        }

        return widths;
    }

    private static string FormatNumber(double number) =>
        Math.Round(number, (int) AfterDotMaxLength).ToString(CultureInfo.InvariantCulture);

    private double GetLength(double number) =>
        _canvas.MeasureTextSize(FormatNumber(number), FontSize).Width;

    private double CalculateRowHeight(IReadOnlyMatrix matrix) =>
        _canvas.MeasureTextSize(FormatNumber(matrix.Get(0, 0)), FontSize).Height;
}
