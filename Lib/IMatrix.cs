using Helpers;
using Lib.Visitor;

namespace Lib;

public interface IMatrix : IVisitableMatrix, IUndecoratable<IMatrix>, ICloneable<IMatrix>
{
    public void Set(uint row, uint column, double value);
}
