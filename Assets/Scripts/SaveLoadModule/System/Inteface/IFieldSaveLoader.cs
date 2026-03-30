using System.Collections.Generic;
using ItemModule;
using UnityEngine;

namespace SaveLoadModule
{
    public interface IFieldSaveLoader
    {
        public List<(ItemType, IProperty[], Vector2Int)> LoadField();
    }
}