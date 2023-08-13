using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Singleton
{
    public abstract class Singleton<T> : SerializedMonoBehaviour where T : Component
    { 
        private static T instance; 
        private static void SetInstance(T t)
        {
            instance = t;
        }
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject
                        {
                            name = "(singleton) " + typeof(T)
                        };
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
        protected virtual void Awake()
        {
            if (instance == null)
            {
                SetInstance(this as T);
                DontDestroyOnLoad(gameObject);
            }
            else 
                Destroy(gameObject); 
        } 

    }
}