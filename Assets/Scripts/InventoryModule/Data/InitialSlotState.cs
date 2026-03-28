using UnityEngine;

namespace InventoryModule
{
    public struct InitialSlotState
    {
        public Sprite Sprite { get; }
        public bool HasItem { get; }

        public InitialSlotState(Sprite sprite)
        {
            Sprite = sprite;
            HasItem = sprite != null;
        }
    }
}