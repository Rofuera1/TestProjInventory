using System.Collections.Generic;
using ItemModule;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace FieldModule
{
    public interface IFieldItemSystem
    {
        public void CreateRandomItem();
        
        public List<IItem> InitialItems { get; }
        
        public Observable<(IItem, Vector2Int)> SpawnedItem { get; }
    }
}