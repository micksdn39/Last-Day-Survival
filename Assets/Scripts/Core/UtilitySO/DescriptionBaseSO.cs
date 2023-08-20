using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.UtilitySO
{
    /// <summary>
    /// Base class for ScriptableObjects that need a public description field.
    /// </summary>
    public class DescriptionBaseSO : SerializedScriptableObject
    {
        [field: TextArea] public string description;
    }
}
