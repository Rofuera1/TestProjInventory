using System;
using Zenject;

namespace ItemModule
{
    public class Item : IItem
    {
        private ItemType _type;
        private IProperty[] _properties;
        
        public ItemType Type => _type;
        public IProperty[] Properties => _properties;

        public Item(ItemType type, IProperty[] properties)
        {
            _type = type;
            _properties = properties;
        }
    }
}