using System.Collections.Generic;
using System.Threading.Tasks;
using ItemModule;

namespace SaveLoadModule
{
    public interface IInventoryTabLoader
    {
        public Task<InventoryParams> LoadInventory(string tabId);
    }
}