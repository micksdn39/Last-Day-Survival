using UnityEngine;

namespace Core.UtilitySO
{
    /// <summary>
    /// Base class for ScriptableObjects that need a public description field.
    /// </summary>
    public class DescriptionBaseSO : ScriptableObject
    {
        [SerializeField]
        [field: TextArea] public string description { get; private set; }
    }
}
