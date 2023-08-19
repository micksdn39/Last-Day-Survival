using Sirenix.Serialization;
using UnityEngine;

namespace LDS.Inventory.Item
{
    public enum ItemType
    {
        Potion,
        QuestItem,
    }
    [CreateAssetMenu(fileName = "NewItem", menuName = "Item/Item")]  
    public class BasicItemSO : ItemSO
    {
        [OdinSerialize] public ItemType itemType { get; private set; }
    }
}
