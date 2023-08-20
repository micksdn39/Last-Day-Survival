using Core.Scene;
using UnityEngine;

namespace LDS.GamePlay
{
    public class WarpPortal : MonoBehaviour
    {
        [SerializeField] private SceneSO sceneLoad;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            
            sceneLoad.LoadScene();
        }
    }
}
