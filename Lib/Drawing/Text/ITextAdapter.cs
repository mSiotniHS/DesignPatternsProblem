namespace Lib.Drawing.Text;

// public record struct CursorPosition(int Line, int Column);

public interface ITextAdapter
{
    void Write(string value);
    void Write(char value);
    void Clear();
}
