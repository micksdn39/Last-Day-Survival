using System;
using System.Collections.Generic;
using Core.UtilitySO;
using UnityEngine;

namespace LDS.Data
{  
    [System.Serializable]
    public class GameData
    {
        [SerializeField] public List<ItemData> itemData { get; private set; }

        public GameData()
        {
            itemData = new List<ItemData>();
        } 
        public void SaveJson(string saveName)
        {
            FileManager.SaveToDisk(this,saveName);
        } 
        public void LoadJson(string saveName,Action onComplete=null)
        {
            GameData GameData = FileManager.LoadFromDisk<GameData>(saveName);
            SaveData(GameData);
            if(onComplete!=null)
                onComplete?.Invoke(); 
        } 
        public void SaveData(GameData gameData)
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
