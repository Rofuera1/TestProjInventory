using ItemModule;

namespace FieldModule
{
    public interface IFieldItemGenerator
    {
        public IItem CreateRandomItem();
        public IItem CreateItem(ItemType itemType, IProperty[] properties);
    }
}