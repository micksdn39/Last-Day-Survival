using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Singleton
{
    public class SingletonSerializedMonoBehaviour<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
    { 
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                SetInstance(this as T); 
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.Log("[Singleton] Instance '" + typeof(T) +
                          "' already exist." +
                          " Destroy duplicated.");
                Destroy(this.gameObject);
            }
        }

        protected static T _instance; 
        private static void SetInstance(T t)
        {
            _instance = t;
        }
        private static readonly object _lock = new object(); 
        public static T Instance
        {
            get
            {
                if (applicationIsQuitting) 
                    return null; 

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                           " - there should never be more than 1 singleton!" +
                                           " Reopening the scene might fix it.");

                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();  
                        
                            singleton.name = "(serialized singleton) " + typeof(T);
 
                            if (Application.isPlaying)
                                DontDestroyOnLoad(singleton);

                            Debug.Log("[Singleton] An instance of " + typeof(T) +
                                      " is needed in the scene, so '" + singleton +
                                      "' was created with DontDestroyOnLoad."); 
                        }
                        else
                        {
                            Debug.Log("[Singleton] Using instance already created: " +
                                      _instance.gameObject.name);

                            if (Application.isPlaying)
                                DontDestroyOnLoad(_instance);
                        }
                    }

                    return _instance;
                }
            }
        }
 
        private static bool applicationIsQuitting;  
        protected virtual void OnDestroy()
        { 
            if (!Application.isPlaying)
            {
                SetInstance(null);
                return;
            } 
            if (_instance == this) 
                applicationIsQuitting = true; 
        }
    }
}