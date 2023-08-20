using System;
using System.Collections.Generic;
using Core;
using LDS.Data;
using LDS.Inventory.Item;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace LDS.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")] 
    public class InventorySO : SerializedScriptableObject
    { 
        [OdinSerialize] public List<ItemStack> items { get; private set; } = new List<ItemStack>();
        [OdinSerialize] public List<ItemStack> defaultItems { get; private set; }  = new List<ItemStack>();

        public UnityAction OnInventoryUpdate;

        [Button]
        public void ResetInventoryUpdate()
        {
            OnInventoryUpdate = null;
        }

        [Button]
        public void Init()
        {
            if (items == null)
            {
                items = new List<ItemStack>();
            }
            items.Clear();
            foreach (ItemStack item in defaultItems)
            {
                items.Add(new ItemStack(item));
            }
            OnInventoryUpdate?.Invoke();
        }  
        public void Add(ItemSO item, int count = 1)
        {
            if (count <= 0)
                return;

            LogCtrl.Debug($"Add item: {item.itemName} quantity = {count}");
            
            foreach (var currentItemStack in items)
            {
                if (item == currentItemStack.item)
                {
                    currentItemStack.Add(count);
                    OnInventoryUpdate?.Invoke();
                    return;
                }
            } 
            items.Add(new ItemStack(item, count));
            OnInventoryUpdate?.Invoke();
        }
        public void UpdateInventory(ItemSO item, int count = 1)
        {
            if (count <= 0)
                return;

            LogCtrl.Debug($"Update item: {item.itemName} quantity = {count}");
            
            foreach (var currentItemStack in items)
            {
                if (item == currentItemStack.item)
                {
                    currentItemStack.SetQuantity(count);
                    return;
                }
            } 
            items.Add(new ItemStack(item, count));
            OnInventoryUpdate?.Invoke();
        }
        public void Remove(ItemSO item, int count = 1)
        {
            if (count <= 0)
                return;

            foreach (var currentItemStack in items)
            {
                if (currentItemStack.item != item)
                    continue;
                
                currentItemStack.Remove(count); 
                if (currentItemStack.quantity <= 0)
                    items.Remove(currentItemStack); 
                return;
            }  
            OnInventoryUpdate?.Invoke();
        }

        public bool Contains(ItemSO item)
        { 
            foreach (var i in items)
            {
                if (item == i.item)
                {
                    return true;
                }
            } 
            return false;
        }
    }
    [Serializable]
    public class ItemStack
    {
        [OdinSerialize] public ItemSO item { get; private set; } 
        [OdinSerialize] public int quantity { get; private set; }

        public int itemId => item.itemId;
        
        public ItemStack()
        {
            item = null;
            quantity = 0;
        }
        public ItemStack(ItemStack itemStack)
        {
            item = itemStack.item;
            quantity = itemStack.quantity;
        }
        public ItemStack(ItemSO item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        } 
        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }
        public void Add(int quantity)
        {
            this.quantity += quantity;
        }
        public void Remove(int quantity)
        {
            this.quantity -= quantity;
        }
    }
}

