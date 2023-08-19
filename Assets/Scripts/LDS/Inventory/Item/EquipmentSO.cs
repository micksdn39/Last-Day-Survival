using Sirenix.Serialization;
using UnityEngine;

namespace LDS.Inventory.Item
{
    public enum EquipmentType
    {
        Weapon,
        Helmet,
        Boots,
        Gloves,
        Shield,
        Accessories, 
    }
    public enum Rarity
    {
        Common,
        Rare,
        Epic, 
    }
    
    [CreateAssetMenu(fileName = "NewEquipment", menuName = "Item/Equipment")] 
    public class EquipmentSO : ItemSO
    {
        [OdinSerialize] public EquipmentType equipmentType { get; private set; }
        [OdinSerialize] public Rarity rarity { get; private set; } 
        
        public EquipmentSO()
        {
            ItemType = ItemType.Equipment;
        } 
    }
}
