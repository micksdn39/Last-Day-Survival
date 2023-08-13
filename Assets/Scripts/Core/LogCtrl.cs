using UnityEngine;

namespace Core
{
    /// <summary>
    /// This Make Easy to Control All Debug Log
    /// </summary>
    public class LogCtrl : MonoBehaviour
    {
        public static void Debug(string log)
        {
            UnityEngine.Debug.Log(log);
        }

        public static void Warning(string log)
        {
            UnityEngine.Debug.LogWarning(log);
        }

        public static void Error(string log)
        {
            UnityEngine.Debug.LogError(log);
        }
    }
}
