namespace Lib.Visitor.Decorators;

public class PlacementAlteringVisitor : IElementVisitor
{
    public delegate (uint, uint) CoordinateAlternator(uint row, uint column);

    private readonly IElementVisitor _visitor;
    private readonly CoordinateAlternator _alternator;

    public PlacementAlteringVisitor(IElementVisitor visitor, CoordinateAlternator alternator)
    {
        _visitor = visitor;
        _alternator = alternator;
    }

    public void VisitElement(uint row, uint column, double element)
    {
        var (alteredRow, alteredColumn) = _alternator(row, column);
        _visitor.VisitElement(alteredRow, alteredColumn, element);
    }

    public void VisitSparseElement(uint row, uint column, double element)
    {
        var (alteredRow, alteredColumn) = _alternator(row, column);
        _visitor.VisitSparseElement(alteredRow, alteredColumn, element);
    }
}
