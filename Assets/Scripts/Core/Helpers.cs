using System.Collections.Generic; 
using UnityEngine; 
using Object = UnityEngine.Object;

namespace Core
{
   public static class Helpers
   {  
      private static Camera _mainCamera;
      public static Camera MainCamera
      {
         get
         {
            if (_mainCamera == null)
            {
               _mainCamera = Camera.main;
            }
            return _mainCamera;
         }
      } 
      private static readonly Dictionary<float,WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
      public static WaitForSeconds GetWait(float time)
      {
         if(WaitDictionary.TryGetValue(time,out var wait))
            return wait;
         WaitDictionary[time] = new WaitForSeconds(time);
         return WaitDictionary[time];
      }

      public static void DeleteAllChildren(this Transform transform)
      {
         foreach (Transform child in transform)
         {  
            Object.Destroy(child.gameObject);
         }
      } 
      public static string RemoveFilenameExtension(this string str)
      {
         int index = str.LastIndexOf('.');
         if (index < 0)
            return str;
        
         return str.Remove(index);
      } 
   }
}
