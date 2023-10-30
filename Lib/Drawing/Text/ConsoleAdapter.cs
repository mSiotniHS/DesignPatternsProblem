namespace Lib.Drawing.Text;

public class ConsoleAdapter : ITextAdapter
{
    public void Write(string value) => Console.Write(value);
    public void Write(char value) => Console.Write(value);
    public void Clear() => Console.Clear();
}
