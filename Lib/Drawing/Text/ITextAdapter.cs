namespace Lib.Drawing.Text;

public interface ITextAdapter
{
    void Write(string value);
    void Write(char value);
    void Clear();
}
