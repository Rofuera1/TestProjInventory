using UnityEngine;

namespace SaveLoadModule
{
    [System.Serializable]
    public class SaveData
    {
        [SerializeField]
        public InventorySlotSave[] InventoryData;
        [SerializeField]
        public WorldSave[] WorldData;
    }
}