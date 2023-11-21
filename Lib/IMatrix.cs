namespace Lib;

public interface IMatrix : IReadOnlyMatrix, IVisitableMatrix, IUndecoratable<IMatrix>
{
    public void Set(uint row, uint column, double value);
}
