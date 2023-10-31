namespace Lib.Drawing.Text;

public record struct PointerPosition(int Line, int Column);

public interface ITextAdapter
{
    void Write(string value, PointerPosition startingPosition);
    void Write(char value, PointerPosition position);
}
