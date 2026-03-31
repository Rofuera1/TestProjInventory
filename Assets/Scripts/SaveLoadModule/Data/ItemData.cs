using System;
using ItemModule;

namespace SaveLoadModule
{
    [Serializable]
    public class ItemData
    {
        public ItemType Type;
        public PropertyData[] Properties;
    }
}