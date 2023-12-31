using System.Numerics;

namespace Helpers;

public static class Operators
{
    public static T Add<T>(T first, T second)
        where T : INumber<T> => first + second;
}
