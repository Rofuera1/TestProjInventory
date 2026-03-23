using Zenject;

namespace ItemModule
{
    public interface IItem
    {
        public ItemType Type { get; }
        public IProperty[] Properties { get; }

        public class ItemFactory : PlaceholderFactory<ItemType, IProperty[]>
        {
            
        }
    }
}