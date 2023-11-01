using Lib.Drawing.Common;

namespace Lib.Drawing.Text;

public interface ITextarea
{
    void Write(string value, PointerPosition startingPosition);
    void Write(char value, PointerPosition position);
}
