using System;
using Core.UtilitySO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Core.Scene
{ 
    [CreateAssetMenu(fileName = "NewGameScene", menuName = "Scene Data/GameScene")]
    public class SceneSO : DescriptionBaseSO
    {
        public enum GameSceneType
        { 
            Default = 0,
            Menu = 1, 
            PersistentManagers = 2,
            Gameplay = 3, 
        } 
       [SerializeField] public GameSceneType sceneType { get; private set; }
       [SerializeField] public AssetReference sceneReference { get; private set; } 
     
       public void LoadSceneAdditive(Action callback=null)
       {
           UiHelpers.KillAllButton();
           sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true).Completed += handle =>
           {
               if(callback!=null)
                   callback?.Invoke(); 
           };
       } 
       public void LoadScene(Action callback=null)
       {
           UiHelpers.KillAllButton();
           sceneReference.LoadSceneAsync().Completed += handle =>
           {
               if(callback!=null)
                   callback?.Invoke();
           };  
       }  
       public bool IsCurrentScene(SceneSO sceneSo)
       {
           return SceneManager.GetActiveScene().name == sceneSo.sceneReference.editorAsset.name;
       }
    }
} 