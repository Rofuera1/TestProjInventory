using System.Collections.Generic;
using ItemModule;
using UnityEngine;

namespace SaveLoadModule
{
    public class SaveLoaderSystem : IFieldSaveLoader, IInventoryTabSaveLoader, ISaveLoader
    {
        private bool _saving;

        private SaveData _saveDataCache;
        
        public List<(ItemType, IProperty[], Vector2Int)> LoadField()
        {
            return new();
        }

        public List<(IItem, int)> LoadInventory(string tabId)
        {
            return new();
        }

        public void Save()
        {
            if (_saving) return;
        }

        private void Load()
        {
            
        }
    }
}