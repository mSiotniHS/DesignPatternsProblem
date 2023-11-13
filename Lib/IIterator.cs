namespace Lib;

/// <summary>
/// Интерфейс для реализации паттерна "Итератор".
/// Представляет собой объект
/// </summary>
/// <typeparam name="TCollection"></typeparam>
/// <typeparam name="TElement"></typeparam>
public interface IIterator<TCollection, out TElement>
{
    TCollection Collection { get; init; }
    TElement GetNext();
    bool HasNext();
}
