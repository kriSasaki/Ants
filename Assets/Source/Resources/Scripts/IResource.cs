using System;

public interface IResource<T>
{
    event Action<T, T> Changed;

    ResourceType Type { get; }
    T Amount { get; }
}
