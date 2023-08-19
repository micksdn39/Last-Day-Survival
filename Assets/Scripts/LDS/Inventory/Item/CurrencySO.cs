using UnityEngine;

namespace LDS.Inventory.Item
{     
    [CreateAssetMenu(fileName = "NewCurrency", menuName = "Item/Currency")]
    public class CurrencySO : ItemSO
    {  
        public CurrencySO()
        {
            ItemType = ItemType.Currency;
        } 
    }
}
