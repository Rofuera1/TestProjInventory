using System;
using ItemModule;

namespace ItemContainerModule
{
    public interface IContainerSlotted : IItemContainer
    {
        public bool IsCellEmpty(int index);
        public bool TryGetItemIndex(IItem item, out int index);

        public bool TryAddItem(IItem item);
        public bool TryAddItem(IItem item, int index);
        public bool TryRemoveItem(IItem item);
        
        public event Action<IItem, int> ItemAdded;
        public event Action<IItem, int> ItemRemoved;
    }
}