using Helpers;

namespace Lib;

public class SparseVector : IVector
{
    private readonly DefaultFirstCollection<uint, double> _nonZeroValues;

    public SparseVector(uint dimension)
    {
        _nonZeroValues = new DefaultFirstCollection<uint, double>(0);
        Dimension = dimension;
    }

    public void Set(uint idx, double value)
    {
        AssertIndex(idx);
        _nonZeroValues.Set(idx, value);
    }

    public double Get(uint idx)
    {
        AssertIndex(idx);
        return _nonZeroValues.Get(idx);
    }

    public uint Dimension { get; }

    private void AssertIndex(uint idx)
    {
        if (idx >= Dimension)
        {
            throw new IndexOutOfRangeException("Referring index is out of range");
        }
    }
}
