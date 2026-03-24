using ItemModule;
using ObservableCollections;

namespace ItemContainerModule
{
    public class ItemContainerSlottedViewModel
    {
        private ObservableList<ContainerSlot> _items;

        public IReadOnlyObservableList<ContainerSlot> Items => _items;
        
        [Zenject.Inject]
        private ItemContainerSlottedViewModel(IContainerSlotted container)
        {
            _items = new();
            foreach (var item in container.Items)
            {
                container.TryGetItemIndex(item, out var position);
                _items.Add(new ContainerSlot() { Item = item, Position = position });
            }
            
            container.ItemAdded += AddItem;
            container.ItemRemoved += RemoveItem;
        }

        private void AddItem(IItem item, int slot)
        {
            _items.Add(new ContainerSlot() { Item = item, Position = slot });
        }

        private void RemoveItem(IItem item, int slot)
        {
            _items.Remove(new ContainerSlot() { Item = item, Position = slot });
        }
    }
}