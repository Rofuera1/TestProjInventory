using ItemModule;

namespace SaveLoadModule
{
    public interface IItemSaveMapper
    {
        public ItemData ToData(IItem item);
        public IItem FromData(ItemData data);
    }
}