using System.Globalization;

namespace Lib.Drawing.Text;

public class MatrixTextDrawer : IMatrixDrawer
{
    private const uint ElementGap = 1;
    private const uint AfterDotMaxLength = 3;

    private readonly ITextarea _textarea;

    private uint[]? _columnWidths;

    public MatrixTextDrawer(ITextarea textarea)
    {
        _textarea = textarea;
        _columnWidths = null;
    }

    public void DrawBraces(IReadOnlyMatrix matrix)
    {
        _columnWidths ??= CalculateColumnWidths(matrix);

        var braceHeight = matrix.RowCount + (matrix.RowCount - 1) * ElementGap + 2;
        var rightBraceX =
            _columnWidths.Aggregate(0u, (sum, next) => sum + next)
            + (matrix.ColumnCount - 1) * ElementGap
            + 1;

        for (var row = 0u; row < braceHeight; row++)
        {
            var (leftBraceSymbol, rightBraceSymbol) = row switch
            {
                0u => ('┌', '┐'),
                _ when row + 1 == braceHeight => ('└', '┘'),
                _ => ('|', '|')
            };

            _textarea.Write(leftBraceSymbol, new PointerPosition((int) row, 0));
            _textarea.Write(rightBraceSymbol, new PointerPosition((int) row, (int) rightBraceX));
        }
    }

    public void DrawElement(uint row, uint column, IReadOnlyMatrix matrix)
    {
        _columnWidths ??= CalculateColumnWidths(matrix);

        var someColumnWidthsSum = _columnWidths
            .Take((int) column)
            .Aggregate(0u, (sum, next) => sum + next);
        var startingX = 1 + someColumnWidthsSum + column;
        var y = 1 + row * (ElementGap + 1);

        var formattedString = FormatNumber(matrix.Get(row, column), AfterDotMaxLength).PadLeft((int) _columnWidths[column]);

        _textarea.Write(formattedString, new PointerPosition((int) y, (int) startingX));
    }

    private static uint[] CalculateColumnWidths(IReadOnlyMatrix matrix)
    {
        var widths = new uint[matrix.ColumnCount];

        for (var column = 0u; column < matrix.ColumnCount; column++)
        {
            var maxLength = 0u;

            for (var row = 0u; row < matrix.RowCount; row++)
            {
                var length = GetLength(matrix.Get(row, column), AfterDotMaxLength);
                if (length > maxLength)
                {
                    maxLength = length;
                }
            }

            widths[column] = maxLength;
        }

        return widths;
    }

    private static string FormatNumber(double number, uint afterDotMaxLength)
        => Math
            .Round(number, (int) afterDotMaxLength)
            .ToString(CultureInfo.InvariantCulture);

    private static uint GetLength(double number, uint afterDotMaxLength) =>
        (uint) FormatNumber(number, afterDotMaxLength)
            .Length;
}
