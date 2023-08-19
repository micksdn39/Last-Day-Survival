using LDS.Data;
using Sirenix.OdinInspector;
using Sirenix.Serialization; 

namespace LDS.Inventory
{
    public class InventoryManager : SerializedMonoBehaviour
    {
        [OdinSerialize] private GameData gameData = null;

    }
}
