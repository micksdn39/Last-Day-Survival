using System.Collections.Generic;
using Core;
using Core.UtilitySO;
using UnityEngine;

namespace LDS.Data
{  
    [System.Serializable]
    public class GameData
    {
        [SerializeField] public List<ItemData> itemData { get; private set; } 
   
        public void NewData()
        { 
            itemData = new List<ItemData>
            {
                new ItemData(1, 100000),
                new ItemData(2, 1000000)
            };
        }
        public void SaveJson(string saveName)
        {
            FileManager.SaveToDisk(this,saveName);
        } 
        public void LoadJson(string saveName)
        {
            GameData GameData = FileManager.LoadFromDisk<GameData>(saveName);
            LogCtrl.Debug(GameData.itemData.Count.ToString());
            SaveSettings(GameData);
        } 
        private void SaveSettings(GameData gameData)
        {
            this.itemData = gameData.itemData; 
        }  
    }
    
    [System.Serializable]
    public class ItemData
    {
        [SerializeField] public int itemId { get; private set; }
        [SerializeField] public int quantity { get; private set; } 
        
        public ItemData (int itemId,int quantity)
        {
            this.itemId = itemId;
            this.quantity = quantity;
        }
    }

}
