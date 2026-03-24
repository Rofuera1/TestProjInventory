using System;

namespace ItemModule
{
    public interface IVisible
    {
        public event Action<bool> SetVisible;
        public bool IsVisible { get; }
    }
}