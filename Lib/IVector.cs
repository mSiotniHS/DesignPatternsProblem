namespace Lib;

public interface IVector
{
    public double Get(uint idx);
    public void Set(uint idx, double value);

    public uint Dimension { get; }
}
