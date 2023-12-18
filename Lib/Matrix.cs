using Lib.Visitor;

namespace Lib;

public class Matrix : AMatrix
{
    public Matrix(uint rowCount, uint columnCount) : base(rowCount, columnCount)
    {
    }

    protected override IVector InitializeVector(uint size) => new Vector(size);

    public override void AcceptVisitor(IElementVisitor visitor)
    {
        for (var i = 0u; i < RowCount; i++)
        {
            for (var j = 0u; j < ColumnCount; j++)
            {
                visitor.VisitElement(i, j, Get(i, j));
            }
        }
    }

    public override IMatrix Clone()
    {
        var clone = new Matrix(RowCount, ColumnCount);
        for (var i = 0u; i < RowCount; i++)
        {
            for (var j = 0u; j < ColumnCount; j++)
            {
                clone.Set(i, j, Get(i, j));
            }
        }

        return clone;
    }
}
