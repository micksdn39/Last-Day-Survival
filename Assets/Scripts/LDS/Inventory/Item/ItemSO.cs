using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace LDS.Inventory.Item
{
    public abstract class ItemSO : SerializedScriptableObject
    {
        [OdinSerialize] public Sprite itemIcon { get; private set; }
        [OdinSerialize] public string itemName { get; private set; }
        [OdinSerialize] public int itemId { get; private set; } 
        [OdinSerialize][field: TextArea] public string itemDescription { get; private set; }
    }
}
