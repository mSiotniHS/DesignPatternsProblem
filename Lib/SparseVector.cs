namespace Lib;

public class SparseVector : IVector
{
    private readonly Dictionary<uint, double> _nonZeroValues;

    public SparseVector(uint dimension)
    {
        _nonZeroValues = new Dictionary<uint, double>();
        Dimension = dimension;
    }

    public void Set(uint idx, double value)
    {
        AssertIndex(idx);

        if (value != 0)
        {
            _nonZeroValues[idx] = value;
        }
        else if (value == 0 && _nonZeroValues.ContainsKey(idx))
        {
            _nonZeroValues.Remove(idx);
        }
    }

    public double Get(uint idx)
    {
        AssertIndex(idx);

        return _nonZeroValues.GetValueOrDefault(idx, 0);
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
