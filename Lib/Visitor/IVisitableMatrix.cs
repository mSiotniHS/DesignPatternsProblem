namespace Lib.Visitor;

public interface IVisitableMatrix : IReadOnlyMatrix
{
	void AcceptVisitor(IElementVisitor visitor);
}
