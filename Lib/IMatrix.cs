using Lib.Visitor;

namespace Lib;

public interface IMatrix : IVisitableMatrix, IUndecoratable<IMatrix>
{
    public void Set(uint row, uint column, double value);
}
