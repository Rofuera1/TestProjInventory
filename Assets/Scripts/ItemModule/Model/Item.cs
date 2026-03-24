using System;
using Zenject;

namespace ItemModule
{
    public class Item : IItem, IVisible
    {
        private ItemType _type;
        private IProperty[] _properties;
        private bool _isVisible;
        
        public ItemType Type => _type;
        public IProperty[] Properties => _properties;
        
        public event Action<bool> SetVisible;
        public bool IsVisible => _isVisible;
    }
}