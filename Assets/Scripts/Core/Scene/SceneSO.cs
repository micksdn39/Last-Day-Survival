using System;
using Core.UtilitySO;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Core.Scene
{ 
    public enum GameSceneType
    { 
        Default = 0,
        Menu = 1, 
        PersistentManagers = 2,
        Gameplay = 3, 
    } 
    
    [CreateAssetMenu(fileName = "NewGameScene", menuName = "Scene Data/GameScene")]
    public class SceneSO : DescriptionBaseSO
    {
        [OdinSerialize] public GameSceneType sceneType { get; private set; }
        [SerializeField] private AssetReference sceneReference;

       private string sceneName => sceneReference.Asset.name;

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

    }
} 