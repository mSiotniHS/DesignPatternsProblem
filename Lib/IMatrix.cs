namespace Lib;

public interface IMatrix : IReadOnlyMatrix
{
    public void Set(uint row, uint column, double value);
}
