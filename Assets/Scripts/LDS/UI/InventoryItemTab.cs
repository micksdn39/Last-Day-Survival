using LDS.Inventory;
using LDS.Inventory.Item;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LDS.UI
{
    public class InventoryItemTab : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private Image icon;

        public UnityAction<ItemStack> OnClickItemTab;
        private ItemStack itemInfo;
        public void RefreshUi(ItemStack item)
        {
            quantityText.gameObject.SetActive(true);
            icon.enabled = true;
            itemInfo = item;
            if (itemInfo == null) return;
            
            ItemSO itemSo = itemInfo.item;
            if(itemSo.itemIcon != null) 
                icon.sprite = itemSo.itemIcon;
            else
                icon.enabled = false;
            
            if (itemInfo.quantity > 1)
                quantityText.text = itemInfo.quantity.ToString();
            else
                quantityText.gameObject.SetActive(false);
        }

        public void OnButtonClick()
        {
            if (itemInfo == null) return;
            
            OnClickItemTab?.Invoke(itemInfo);
        }
    }
}
