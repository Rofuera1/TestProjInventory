using System.Collections.Generic;
using ItemModule;

namespace SaveLoadModule
{
    public interface IInventoryTabSaveLoader
    {
        public List<(IItem, int)> LoadInventory(string tabId);
    }
}