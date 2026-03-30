using ItemLibraryModule;
using ItemModule;
using JetBrains.Annotations;

namespace FieldModule
{
    public class FieldItemGenerator: IFieldItemGenerator
    {
        private IItemLibrarySystem _librarySystem;
        private ItemFactory _itemFactory;

        public FieldItemGenerator(ItemFactory itemFactory, IItemLibrarySystem librarySystem)
        {
            _librarySystem = librarySystem;
            _itemFactory = itemFactory;
        }
        
        [CanBeNull]
        public IItem CreateRandomItem()
        {
            var (randomType, randomProperties) = _librarySystem.GetRandomProperties();
            var newItem = _itemFactory.Create(randomType, randomProperties);

            return newItem;
        }

        [CanBeNull]
        public IItem CreateItem(ItemType itemType, IProperty[] properties)
        {
            var newItem = _itemFactory.Create(itemType, properties);

            return newItem;
        }
    }
}