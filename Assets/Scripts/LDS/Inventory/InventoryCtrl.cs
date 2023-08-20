using System;
using System.Collections.Generic;
using LDS.Inventory.Item;
using LDS.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LDS.Inventory
{
    public class InventoryCtrl : MonoBehaviour
    {
        [SerializeField] private InventorySO inventory;
        [Space]
        [Title("UI")]
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [Space]
        [Title("Inventory Tab")]
        [SerializeField] private InventoryItemTab prefabInventoryItemTab;
        [SerializeField] private Transform contentInventoryItemTab;
        [SerializeField] private List<InventoryItemTab> listOfInventoryItemTabs;

        private void OnDestroy()
        {
            foreach (var inventoryTab in listOfInventoryItemTabs)
            { 
                inventoryTab.OnClickItemTab -= OnClickItemTab;
            }
        }

        public void InitPanel()
        {
            icon.enabled = false;
            descriptionText.gameObject.SetActive(false);
            
            prefabInventoryItemTab.gameObject.SetActive(false);
            foreach (var inventoryTab in listOfInventoryItemTabs)
            {
                inventoryTab.gameObject.SetActive(false);
            }

            int index = -1;
            foreach (var item in inventory.items)
            {
                index++;
                if (listOfInventoryItemTabs.Count > index)
                {
                    listOfInventoryItemTabs[index].RefreshUi(item);
                    listOfInventoryItemTabs[index].gameObject.SetActive(true);
                    continue;
                }

                InventoryItemTab inventoryTab = Instantiate(prefabInventoryItemTab, contentInventoryItemTab);
                inventoryTab.RefreshUi(item);
                inventoryTab.gameObject.SetActive(true);
                inventoryTab.OnClickItemTab += OnClickItemTab;
                listOfInventoryItemTabs.Add(inventoryTab);
            }
            
            
        } 
        private void OnClickItemTab(ItemStack item)
        { 
            descriptionText.gameObject.SetActive(true);
            icon.enabled = true;
            
            ItemSO itemSo = item.item;
            if(itemSo.itemIcon != null) 
                icon.sprite = itemSo.itemIcon;
            else
                icon.enabled = false;

            descriptionText.text = itemSo.itemDescription;
        }
    }
}
