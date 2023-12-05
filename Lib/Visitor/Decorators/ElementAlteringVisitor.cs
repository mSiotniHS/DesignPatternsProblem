namespace Lib.Visitor.Decorators;

public class ElementAlteringVisitor : IElementVisitor
{
	public delegate double ElementAlternator(uint row, uint column);

	private readonly IElementVisitor _visitor;
	private readonly ElementAlternator _get;

	public ElementAlteringVisitor(IElementVisitor visitor, ElementAlternator get)
	{
		_visitor = visitor;
		_get = get;
	}

	public void VisitElement(uint row, uint column, double element) =>
		_visitor.VisitElement(row, column, _get(row, column));

	public void VisitSparseElement(uint row, uint column, double element) =>
		_visitor.VisitSparseElement(row, column, _get(row, column));
}
