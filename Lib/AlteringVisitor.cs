namespace Lib;

public class AlteringVisitor : IElementVisitor
{
	private readonly IElementVisitor _visitor;
	private readonly Func<uint, uint, double> _get;

	public AlteringVisitor(IElementVisitor visitor, Func<uint, uint, double> get)
	{
		_visitor = visitor;
		_get = get;
	}

	public void VisitElement(uint row, uint column, double element)
	{
		_visitor.VisitElement(row, column, _get(row, column));
	}

	public void VisitSparseElement(uint row, uint column, double element)
	{
		_visitor.VisitSparseElement(row, column, _get(row, column));
	}
}
