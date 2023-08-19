using System.Collections.Generic;
using System.IO; 
using Core;
using Sirenix.OdinInspector; 
#if UNITY_EDITOR 
using UnityEditor;
#endif
using UnityEngine;

namespace LDS.Inventory.Item
{    
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item/All")] 
    public class ItemDB : SerializedScriptableObject
    {
        [SerializeField] public List<ItemSO> availableItem = new List<ItemSO>();

        [FolderPath] public string[] folderPath;
        
        private readonly string metaFile = "meta";
        private readonly string assetFile = "asset";

        public ItemSO GetItem(int itemId)
        {
            var item = availableItem.Find(x => x.itemId == itemId);
            if(item == null)
                LogCtrl.Error($"ItemID {itemId} Not Found!");
            return availableItem.Find(x => x.itemId == itemId);
        }
        
#if UNITY_EDITOR 
        [Button]
        public void LoadAllItem()
        {
            if (availableItem == null)
                availableItem = new List<ItemSO>();
            availableItem.Clear();
            foreach (var folder  in folderPath)
            { 
                if (Directory.Exists(folder))
                {
                    var info = new DirectoryInfo(folder);
                    var fileInfo = info.GetFiles();

                    foreach (var file  in fileInfo)
                    {
                        if(file.Name.Contains(metaFile))
                            continue;
                            
                        if (file.Name.Contains(assetFile))
                        {  
                            string path = $"{folder}/" + file.Name;
                            LogCtrl.Debug(path);
                            ItemSO fileSO = (ItemSO)AssetDatabase.LoadAssetAtPath(path,typeof(ItemSO)); 
                            availableItem.Add(fileSO); 
                        }
                    }
                }
            } 
#endif
        }
    }
}
