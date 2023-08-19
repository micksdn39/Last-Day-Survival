using System;
using Core;
using LDS.Inventory;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace LDS.Data
{
    public class SaveManager : SerializedMonoBehaviour
    {
        [SerializeField] private string saveFilename = "LDS_Save";
        [SerializeField] private InventorySO inventory;
        
        [OdinSerialize,ReadOnly] private GameData gameData = null;

        [HideInInspector] public Action<GameData> onSaveGame;
        [HideInInspector] public Action<GameData> onLoadGame;
  
        [Button]
        public void NewGameTest()
        { 
            CheckFileSave();
            gameData.NewData(); 
        }
        [Button]
        public void SaveGame()
        {
            CheckFileSave();
            gameData.SaveJson(saveFilename); 
            onSaveGame?.Invoke(gameData);
        }
        [Button]
        public void LoadSave()
        { 
            CheckFileSave();
            gameData.LoadJson(saveFilename);
            onLoadGame?.Invoke(gameData);
        }
        [Button]
        public void OpenURL()
        {   
            Application.OpenURL(Application.persistentDataPath); 
            LogCtrl.Debug(gameData.ToString());
        }

        private void UpdateInventory()
        {
             
        }
        private void CheckFileSave()
        {
            if (gameData != null)
                return;
            gameData = new GameData();
            LogCtrl.Debug("New Game Data");
        }
    }
}
