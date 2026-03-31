using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks; // unitask could've been here
using ItemModule;
using UnityEngine;

namespace SaveLoadModule
{
    public class LoadSystem : IFieldLoader, IInventoryTabLoader
    {
        private IItemSaveMapper _itemSaveMapper;
        
        private SaveData _loadedDataCache;

        private bool _loading;
        private TaskCompletionSource<bool> _loadingCompleted = new();

        public LoadSystem(IItemSaveMapper itemSaveMapper)
        {
            _itemSaveMapper = itemSaveMapper;
            LoadAll();
        }

        private void LoadAll()
        {
            if (_loading) return;
            _loading = true;

            if (File.Exists(SaveSystem.PATH))
            {
                var json = File.ReadAllText(SaveSystem.PATH);
                _loadedDataCache = string.IsNullOrWhiteSpace(json)
                    ? new SaveData()
                    : JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
            }
            
            Debug.Log($"Loaded null ? {_loadedDataCache == null}");
            
            _loadingCompleted.SetResult(true);
            _loading = false;
        }
        
        public async Task<List<(IItem, Vector2Int)>> LoadField()
        {
            if (_loading)
                await _loadingCompleted.Task;

            if (_loadedDataCache?.WorldData == null)
                return new ();
            
            var field = _loadedDataCache.WorldData;
            
            var result = new List<(IItem, Vector2Int)>();
            foreach (var item in field)
            {
                var itemConverted = _itemSaveMapper.FromData(item.Item);
                result.Add((itemConverted, item.Position));
            }

            return result;
        }

        public async Task<InventoryParams> LoadInventory(string tabId)
        {
            if (_loading)
                await _loadingCompleted.Task;

            if (_loadedDataCache?.InventoryData == null)
                return new();
            
            var tabSaveData = _loadedDataCache.InventoryData.FirstOrDefault(t => t.TabId == tabId);
            if (tabSaveData == null) return new();
            
            var result = new List<InventoryData>();
            foreach (var item in tabSaveData.ItemsData)
            {
                var itemConverted = _itemSaveMapper.FromData(item.Item);
                result.Add(new() {
                    Item = itemConverted, 
                    Slot = item.Slot
                    }
                );
            }

            return new()
            {
                Capacity = tabSaveData.Capacity,
                Id = tabId,
                Items = result
            };
        }
    }
}