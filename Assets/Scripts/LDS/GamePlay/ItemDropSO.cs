using System;
using System.Collections.Generic;
using Core;
using LDS.Inventory.Item;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LDS.GamePlay
{
    [CreateAssetMenu(fileName = "New Drop List", menuName = "Item/Item Drop List")]
    public class ItemDropSO : SerializedScriptableObject
    {
        [OdinSerialize] public List<ItemDropInfo> itemDropInfos { get; private set; }

        public List<ItemSO> GetDrop(float dropItemIncrease=0)
        {
            float currentPlayerDropRate = 1;
            currentPlayerDropRate += dropItemIncrease;

            List<ItemSO> item = new List<ItemSO>();
            
            foreach (var itemDrop in itemDropInfos)
            {
                float random = Random.Range(0, 100);
                float resultRate = itemDrop.dropRate * currentPlayerDropRate;
                if(random<=resultRate)
                {
                    LogCtrl.Debug("You drop : "+itemDrop.itemDrop.itemName);
                    item.Add(itemDrop.itemDrop);
                }
            } 
            if(item.Count == 0)
                LogCtrl.Debug("You don't have any drop item"); 

            return item;
        } 
    }

    [Serializable]
    public class ItemDropInfo
    {
        [OdinSerialize] public ItemSO itemDrop { get; private set; }
        
        [OdinSerialize] public float dropRate { get; private set; }

        public ItemDropInfo(ItemSO itemDrop, float dropRate)
        {
            this.itemDrop = itemDrop;
            this.dropRate = dropRate;
        }
        
    }
    
    
}
