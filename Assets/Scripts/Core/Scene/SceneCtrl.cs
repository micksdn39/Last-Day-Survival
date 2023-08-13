using System;
using UnityEngine; 
using UnityEngine.SceneManagement; 

namespace Core.Scene
{    
    public class SceneCtrl : MonoBehaviour
    {  
        public static void LoadScene(string sceneName,Action callback=null)
        {
            UiHelpers.KillAllButton();
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if(scene.name == sceneName && callback!=null)
                    callback?.Invoke();
            };
            SceneManager.LoadScene(sceneName);  
        }  
    }
}