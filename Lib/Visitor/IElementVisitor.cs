namespace Lib.Visitor;

public interface IElementVisitor
{
	void VisitElement(uint row, uint column, double element);
	void VisitSparseElement(uint row, uint column, double element);
}
