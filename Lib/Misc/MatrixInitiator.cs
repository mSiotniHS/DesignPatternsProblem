namespace Lib.Misc;

public static class MatrixInitiator
{
    public static void FillMatrix(IMatrix matrix, uint nonZeroValuesCount, double maxValue)
    {
        var valuesCount = matrix.RowCount * matrix.ColumnCount;

        var nonZeroValues = Enumerable
            .Range(0, (int)nonZeroValuesCount)
            .Select(_ => Random.Shared.NextDouble() * maxValue);
        var zeroValues = Enumerable
            .Repeat(0.0, (int)(valuesCount - nonZeroValuesCount));

        var elements = nonZeroValues
            .Concat(zeroValues)
            .OrderBy(_ => Random.Shared.Next())
            .ToArray();

        for (var i = 0u; i < matrix.RowCount; i++)
        {
            for (var j = 0u; j < matrix.ColumnCount; j++)
            {
                matrix.Set(i, j, elements[i * matrix.ColumnCount + j]);
            }
        }
    }
}
