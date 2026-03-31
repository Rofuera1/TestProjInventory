using System.Collections.Generic;
using System.Threading.Tasks;
using ItemModule;
using R3;
using SaveLoadModule;
using UnityEngine;

namespace FieldModule
{
    public class FieldItemSystem : IFieldItemSystem
    {
        private IFieldItemGenerator _fieldItemGenerator;
        private IFieldSystem _fieldSystem;
        private IFieldLoader _fieldLoader;

        public readonly List<IItem> _initialItems;
        public List<IItem> InitialItems => _initialItems;

        private Subject<(IItem, Vector2Int)> _spawnedItem = new();
        private Subject<IItem> _destroyedItem = new();

        public Observable<(IItem, Vector2Int)> SpawnedItem => _spawnedItem;
        public Observable<IItem> DestroyedItem => _destroyedItem;

        public FieldItemSystem(IFieldItemGenerator fieldItemGenerator, IFieldSystem fieldSystem,
            IFieldLoader fieldLoader)
        {
            _fieldItemGenerator = fieldItemGenerator;
            _fieldSystem = fieldSystem;
            _fieldLoader = fieldLoader;
            
            _initialItems = new List<IItem>();
            LoadItems();
        }

        public void CreateItemFromInventory(IItem item, Vector2Int position)
        {
            _spawnedItem.OnNext((item, position));
        }

        public void DestroyItem(IItem item)
        {
            _destroyedItem.OnNext(item);
        }

        public void CreateRandomItem()
        {
            if (!_fieldSystem.CanPlaceItem()) return;
            
            var item = _fieldItemGenerator.CreateRandomItem();

            if (!_fieldSystem.TryPlaceItem(item, out var position))
            {
                Debug.LogWarning("FieldItemSystem: item was created, but could not be placed on the field.");
                return;
            }
            
            _spawnedItem.OnNext((item, position));
        }

        private async Task LoadItems()
        {
            var items = await _fieldLoader.LoadField();
            foreach (var item in items)
            {
                var createdItem = _fieldItemGenerator.CreateItem(item.Item1.Type, item.Item1.Properties);
                _fieldSystem.TryPlaceItem(item.Item2, createdItem);
                _initialItems.Add(createdItem);
            }
        }
    }
}
