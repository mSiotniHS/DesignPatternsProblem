namespace Lib;

public class DrawingStrategyCreator<T> : IDrawingStrategyCreator
    where T : class, IDrawingStrategy, new()
{
    public IDrawingStrategy CreateStrategy(IReadOnlyMatrix matrix)
    {
        var strategy = new T
        {
            Matrix = matrix
        };
        return strategy;
    }
}
