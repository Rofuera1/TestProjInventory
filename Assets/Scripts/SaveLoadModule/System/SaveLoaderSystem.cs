using System.Collections.Generic;
using ItemModule;
using UnityEngine;

namespace SaveLoadModule
{
    public class SaveLoaderSystem : IFieldSaveLoader, IInventoryTabSaveLoader
    {
        public List<(ItemType, IProperty[], Vector2Int)> LoadField()
        {
            return new();
        }

        public List<(IItem, int)> LoadInventory(string tabId)
        {
            return new();
        }
    }
}