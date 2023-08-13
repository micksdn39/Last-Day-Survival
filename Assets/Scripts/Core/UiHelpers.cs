using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core
{
    public class UiHelpers : MonoBehaviour
    {
        public static void KillAllButton()
        {
            Button[] btns = FindObjectsOfType<Button>();
            foreach (var btn in btns)
                btn.enabled = false;
        }  
        
        private static PointerEventData _pointerEventData;
        private static List<RaycastResult> _results;

        public static bool IsOverUi()
        {
            _pointerEventData = new PointerEventData(EventSystem.current){position = Input.mousePosition};
            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_pointerEventData, _results);
            return _results.Count > 0;
        }
    }
}
