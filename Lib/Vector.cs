namespace Lib;

public class Vector : IVector
{
    private readonly double[] _vector;

    public Vector(uint dimension)
    {
        _vector = new double[dimension];
        Dimension = dimension;
    }

    public void Set(uint idx, double value) => _vector[idx] = value;

    public double Get(uint idx) => _vector[idx];

    public uint Dimension { get; }
}
