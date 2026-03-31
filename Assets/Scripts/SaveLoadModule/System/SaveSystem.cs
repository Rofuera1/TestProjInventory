using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace SaveLoadModule
{
    
    public class SaveSystem: ISaveSystem
    {
        public static string PATH = Path.Combine(Application.persistentDataPath, "save.json"); // couldve done in scriptable, but i have no time!!!
        
        private bool _saving;

        private IFieldSaveSnapshot _fieldSaveSnapshot;
        private IInventoryTabSaveSnapshot[] _inventorySnapshots;

        private IItemSaveMapper _itemSaveMapper;

        public SaveSystem(IFieldSaveSnapshot fieldSaveSnapshot, IInventoryTabSaveSnapshot[] inventorySnapshots, IItemSaveMapper itemSaveMapper)
        {
            _fieldSaveSnapshot = fieldSaveSnapshot;
            _inventorySnapshots = inventorySnapshots;
            _itemSaveMapper = itemSaveMapper;
        }
        
        public async Task Save()
        {
            Debug.Log($"Saving... (Is other saving running ?= {_saving})");
            if (_saving) return;
            _saving = true;

            var worldData = new List<WorldSave>();
            
            try
            {
                var fieldSnapshot = _fieldSaveSnapshot.GetSnapshot();
                worldData = ConvertWorldData(fieldSnapshot);
            }
            catch (Exception e)
            {
                Debug.Log($"Saving failed at gathering snapshot from field with exception {e}");
                _saving = false;
                throw;
            }

            var slotSaves = new List<InventorySlotSave>();

            try
            {
                foreach (var snapshotTab in _inventorySnapshots)
                {
                    var snapshot = snapshotTab.GetSnapshot();
                    var itemsInSlot = ConvertInventoryData(snapshot.Items);
                    slotSaves.Add(new InventorySlotSave()
                    {
                        ItemsData = itemsInSlot.ToArray(),
                        TabId = snapshot.Id,
                        Capacity = snapshot.Capacity,
                    });
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Saving failed at gathering snapshots from tabs with exception {e}");
                _saving = false;
                throw;
            }

            var saveData = new SaveData()
            {
                InventoryData = slotSaves.ToArray(),
                WorldData = worldData.ToArray(),
            };
            
            Debug.Log($"Collected all snapshots, writing to {PATH}");
            
            var json = JsonUtility.ToJson(saveData, true);
            await File.WriteAllTextAsync(PATH, json);
            
            Debug.Log($"Saved!");

            _saving = false;
        }

        private List<InventorySave> ConvertInventoryData(List<InventoryData> inventoryData)
        {
            var result = new List<InventorySave>();
            
            foreach (var data in inventoryData)
            {
                var converted = _itemSaveMapper.ToData(data.Item);
                result.Add(new()
                {
                    Item = converted,
                    Slot = data.Slot,
                });
            }

            return result;
        }

        private List<WorldSave> ConvertWorldData(List<WorldData> data)
        {
            var result = new List<WorldSave>();
            
            foreach (var worldData in data)
            {
                var converted = _itemSaveMapper.ToData(worldData.Item);
                result.Add(new()
                {
                    Item = converted,
                    Position = worldData.Position,
                });
            }

            return result;
        }
    }
}