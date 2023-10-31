namespace Lib.Drawing;

public class BorderlessDrawer : IMatrixDrawer
{
	private readonly IMatrixDrawer _drawer;

	public BorderlessDrawer(IMatrixDrawer drawer)
	{
		_drawer = drawer;
	}

	public void DrawBraces(IReadOnlyMatrix matrix)
	{
	}

	public void DrawElement(uint row, uint column, IReadOnlyMatrix matrix)
	{
		_drawer.DrawElement(row, column, matrix);
	}
}
