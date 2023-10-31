namespace Lib.Drawing.Text;

public class ConsoleAdapter : ITextAdapter
{
    public void Write(string value, PointerPosition startingPosition)
    {
        Console.SetCursorPosition(startingPosition.Column, startingPosition.Line);
        Console.Write(value);
    }

    public void Write(char value, PointerPosition position)
    {
        Console.SetCursorPosition(position.Column, position.Line);
        Console.Write(value);
    }
}
