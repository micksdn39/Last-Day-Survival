using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace LDS.Inventory.Item
{
    public enum ItemType
    {
        Default,
        Potion,
        Quest,
        Equipment,
        Currency
    }
    public abstract class ItemSO : SerializedScriptableObject
    {
        [OdinSerialize] public ItemType ItemType { get; protected set; } 
        [OdinSerialize] public Sprite itemIcon { get; private set; }
        
        [OdinSerialize] public string itemName { get; private set; }
        [OdinSerialize] public int itemId { get; private set; }
        
        [field: TextArea] public string itemDescription;
          
    }
}
