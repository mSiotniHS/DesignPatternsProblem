namespace Lib;

public interface IVisitableMatrix
{
	void AcceptVisitor(IElementVisitor visitor);
}
