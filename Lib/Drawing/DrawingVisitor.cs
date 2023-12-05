using Lib.Visitor;

namespace Lib.Drawing;

public class DrawingVisitor : IElementVisitor
{
	private readonly IMatrixDrawer _drawer;
	private readonly IReadOnlyMatrix _matrix;

	public DrawingVisitor(IMatrixDrawer drawer, IReadOnlyMatrix matrix)
	{
		_drawer = drawer;
		_matrix = matrix;
	}

	public void VisitElement(uint row, uint column, double element)
	{
		_drawer.DrawElement(row, column, _matrix);
	}

	public void VisitSparseElement(uint row, uint column, double element)
	{
		if (element != 0)
		{
			_drawer.DrawElement(row, column, _matrix);
		}
	}
}
