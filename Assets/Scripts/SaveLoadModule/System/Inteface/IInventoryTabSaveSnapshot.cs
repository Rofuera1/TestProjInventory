using System.Collections.Generic;

namespace SaveLoadModule
{
    public interface IInventoryTabSaveSnapshot
    {
        public InventoryParams GetSnapshot();
    }
}