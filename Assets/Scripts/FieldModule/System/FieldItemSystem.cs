using System.Collections.Generic;
using ItemModule;
using SaveLoadModule;

namespace FieldModule
{
    public class FieldItemSystem : IFieldItemSystem
    {
        private IFieldItemGenerator _fieldItemGenerator;
        private IFieldSystem _fieldSystem;
        private IFieldSaveLoader _fieldSaveLoader;

        public readonly List<IItem> _initialItems;
        public List<IItem> InitialItems => _initialItems;

        public FieldItemSystem(IFieldItemGenerator fieldItemGenerator, IFieldSystem fieldSystem,
            IFieldSaveLoader fieldSaveLoader)
        {
            _fieldItemGenerator = fieldItemGenerator;
            _fieldSystem = fieldSystem;
            _fieldSaveLoader = fieldSaveLoader;
            
            _initialItems = new List<IItem>();
            LoadItems();
        }
        
        public void CreateRandomItem()
        {
            if (!_fieldSystem.CanPlaceItem()) return;
            
            var item = _fieldItemGenerator.CreateRandomItem();
            _fieldSystem.TryPlaceItem(item);
        }

        private void LoadItems()
        {
            var items = _fieldSaveLoader.LoadField();
            foreach (var item in items)
            {
                var createdItem = _fieldItemGenerator.CreateItem(item.Item1, item.Item2);
                _fieldSystem.TryPlaceItem(item.Item3, createdItem);
                _initialItems.Add(createdItem);
            }
        }
    }
}