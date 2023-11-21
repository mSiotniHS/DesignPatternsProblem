namespace Lib;

public interface IDrawingStrategyCreator
{
    IDrawingStrategy CreateStrategy(IReadOnlyMatrix matrix);
}
