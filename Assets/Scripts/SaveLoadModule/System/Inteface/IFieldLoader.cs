using System.Collections.Generic;
using System.Threading.Tasks;
using ItemModule;
using UnityEngine;

namespace SaveLoadModule
{
    public interface IFieldLoader
    {
        public Task<List<(IItem, Vector2Int)>> LoadField();
    }
}