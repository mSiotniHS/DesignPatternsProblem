namespace Lib.Drawing.Text;

public record struct PointerPosition(int Line, int Column);

public interface ITextarea
{
    void Write(string value, PointerPosition startingPosition);
    void Write(char value, PointerPosition position);
}
