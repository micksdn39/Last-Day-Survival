using System;
using Core.UtilitySO;
using EasyTransition;
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
        [SerializeField] private TransitionSettings transition;

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
       public void LoadSceneTransition(Action callback=null)
       { 
           TransitionManager.Instance().onTransitionBegin += UiHelpers.KillAllButton;

           TransitionManager.Instance().onTransitionCutPointReached += Load;
           
           TransitionManager.Instance().onTransitionEnd += () =>
           {
               TransitionManager.Instance().onTransitionCutPointReached -= Load;
               if(callback!=null)
                   callback?.Invoke();
           }; 
           
           TransitionManager.Instance().Transition(transition, 0);

           void Load()
           {
               sceneReference.LoadSceneAsync();  
           }
       }
    }
} 