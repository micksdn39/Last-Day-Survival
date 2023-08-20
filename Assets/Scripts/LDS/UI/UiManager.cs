using System;
using Core.Event;
using UnityEngine;

namespace LDS.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private UnityEventTrigger InventoryPanel; 
        [SerializeField] private MouseLookScript mouseLookScript;

        private MouseLookScript mouse
        {
            get
            {
                if (mouseLookScript == null)
                    mouseLookScript = GameObject.Find("Player").GetComponent<MouseLookScript>();
                return mouseLookScript;
            }
        }
        private void Start()
        {
            InventoryPanel.AddOnDisableEvent(() =>
            {
                GunScript.isCanShoot = true;
                mouse.isMouseMove = true;
                MouseLock(CursorLockMode.Locked); 
            });
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GunScript.isCanShoot = false;
                mouse.isMouseMove = false;
                InventoryPanel.gameObject.SetActive(true);
                MouseLock(CursorLockMode.Confined);
            }
        }

        private void MouseLock(CursorLockMode mode)
        {
            Cursor.lockState = mode;
        }
    }
}
