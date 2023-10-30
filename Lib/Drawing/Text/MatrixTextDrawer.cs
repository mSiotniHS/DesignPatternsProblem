using System.Globalization;

namespace Lib.Drawing.Text;

public class MatrixTextDrawer : IMatrixDrawer
{
    private const uint ElementGap = 1;
    private const uint AfterDotMaxLength = 3;

    private readonly ITextAdapter _adapter;
    private readonly char[,] _state;

    private uint[]? _columnWidths;

    public MatrixTextDrawer(ITextAdapter adapter)
    {
        _adapter = adapter;
        _state = new char[64, 64];
        _columnWidths = null;

        for (var i = 0; i < _state.GetLength(0); i++)
        {
            for (var j = 0; j < _state.GetLength(1); j++)
            {
                _state[i, j] = ' ';
            }
        }
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
            (_state[row, 0], _state[row, rightBraceX]) = row switch
            {
                0u => ('┌', '┐'),
                _ when row + 1 == braceHeight => ('└', '┘'),
                _ => ('|', '|')
            };
        }

        Update();
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

        for (var i = 0; i < _columnWidths[column]; i++)
        {
            _state[y, startingX + i] = formattedString[i];
        }

        Update();
    }

    private void Update()
    {
        _adapter.Clear();

        for (var i = 0; i < _state.GetLength(0); i++)
        {
            for (var j = 0; j < _state.GetLength(1); j++)
            {
                _adapter.Write(_state[i, j]);
            }
            _adapter.Write('\n');
        }
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
