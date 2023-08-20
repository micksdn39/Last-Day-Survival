using System;
using System.Collections.Generic;
using LDS.Inventory;
using LDS.Inventory.Item;
using UnityEngine;

namespace LDS.GamePlay
{
    public class GameManagers : MonoBehaviour
    {
        private static GameManagers _gameManagers;
        public static GameManagers Instance()
        {
            return _gameManagers;
        } 
        private void Awake()
        {
            _gameManagers = this;
        }
         
        [SerializeField] private InventorySO inventory;

        public void UpdateInventory(List<ItemSO> item)
        {
            if(item == null)return;
            if(item.Count == 0)return;
            
            foreach (var i in item)
            {
                inventory.Add(i);
            }
        }
        
        
    }
    
}
