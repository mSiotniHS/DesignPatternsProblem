namespace Lib.Helpers;

public class MatrixStatistics
{
    public double Sum { get; }
    public double Average { get; }
    public double Max { get; }
    public uint NonZeroValuesCount { get; }

    public MatrixStatistics(IReadOnlyMatrix matrix)
    {
        Sum = CalculateSum(matrix);
        Average = CalculateAverage(matrix);
        Max = CalculateMax(matrix);
        NonZeroValuesCount = CalculateNonZeroValuesCount(matrix);
    }

    private static double CalculateSum(IReadOnlyMatrix matrix)
    {
        var sum = 0.0;

        for (var i = 0u; i < matrix.RowCount; i++)
        {
            for (var j = 0u; j < matrix.ColumnCount; j++)
            {
                sum += matrix.Get(i, j);
            }
        }

        return sum;
    }

    private double CalculateAverage(IReadOnlyMatrix matrix) =>
        Sum / (matrix.RowCount * matrix.ColumnCount);

    private static double CalculateMax(IReadOnlyMatrix matrix)
    {
        var maxValue = double.MinValue;

        for (var i = 0u; i < matrix.RowCount; i++)
        {
            for (var j = 0u; j < matrix.ColumnCount; j++)
            {
                var currentValue = matrix.Get(i, j);
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                }
            }
        }

        return maxValue;
    }

    private static uint CalculateNonZeroValuesCount(IReadOnlyMatrix matrix)
    {
        var count = 0u;

        for (var i = 0u; i < matrix.RowCount; i++)
        {
            for (var j = 0u; j < matrix.ColumnCount; j++)
            {
                if (matrix.Get(i, j) != 0)
                {
                    count++;
                }
            }
        }

        return count;
    }
}
