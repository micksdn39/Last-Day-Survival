using System;
using System.Collections.Generic;
using Core;
using LDS.Inventory;
using LDS.Inventory.Item;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace LDS.Data
{
    public class SaveManager : SerializedMonoBehaviour
    {
        [SerializeField] private string saveFilename = "LDS_Save";
        [SerializeField] private ItemDB itemDB; 
        [SerializeField] private InventorySO inventory;

        [OdinSerialize, ReadOnly] private GameData gameData;

        [HideInInspector] public UnityAction onSaveGame; 
        [HideInInspector] public UnityAction<GameData> onLoadGame;

        private void Start()
        {
            LoadSave();
            inventory.OnInventoryUpdate += SaveGame;
        }
        private void OnDestroy()
        {
            SaveGame();
            inventory.OnInventoryUpdate -= SaveGame;
        }
        [Button]
        public void SaveGame()
        {
            CheckGameData();
            
            SaveInventory();
            gameData.SaveJson(saveFilename); 
            onSaveGame?.Invoke();
        }
        [Button]
        public void LoadSave()
        {
            CheckGameData();
            
            void LoadComplete()
            {
                LoadInventory();
                onLoadGame?.Invoke(gameData);
            } 
            gameData.LoadJson(saveFilename,LoadComplete);
        }
        [Button]
        public void OpenURL()
        {   
            Application.OpenURL(Application.persistentDataPath); 
            LogCtrl.Debug(gameData.ToString());
        }

        private void LoadInventory()
        {
            foreach (var item in gameData.itemData)
            {
                var i= itemDB.GetItem(item.itemId);
                if(i != null)
                    inventory.UpdateInventory(i,item.quantity); 
            }
        } 
        private void SaveInventory()
        {
            GameData g = new GameData(); 
            foreach (var item in inventory.items)
            {
                g.itemData.Add(new ItemData(item.itemId,item.quantity));
            }
            gameData.SaveData(g);
        }

        private void CheckGameData()
        {
            if (gameData != null)
                return; 
            gameData = new GameData();
            LogCtrl.Debug("New Game Data");
        }
    }
}
