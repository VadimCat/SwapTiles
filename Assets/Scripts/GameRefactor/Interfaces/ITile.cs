using System;

namespace GameRefactor.Interfaces
{
    public interface ITile
    {
        bool IsCompleted { get; }
        bool IsSelected { get; }

        event Action<bool> EventIsSelectedUpdated;
        void Tap();
    }
}