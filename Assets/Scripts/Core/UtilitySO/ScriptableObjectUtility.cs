using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Core.UtilitySO
{
    public static class ScriptableObjectUtility
    {
        public static T LoadResourceScriptableObject<T>(string name) where T : class
        {
            return Resources.Load("ScriptableObject/" + name) as T;
        }
#if UNITY_EDITOR

        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        //
        // [MenuItem("MenuName/MenuSubName")]
        public static T CreateAsset<T>(string fileName) where T : ScriptableObject
        {
            string fullPath = "Assets/Resources/ScriptableObject/" + fileName + ".asset";
            if (AssetDatabase.LoadAssetAtPath<T>(fullPath) != null)
            {
                T data = AssetDatabase.LoadAssetAtPath<T>(fullPath);
                Selection.activeObject = data;
                return data;
            } 
            PrepareFolders("Assets/Resources/ScriptableObject/");
            CreateResourceAsset<T>(fullPath);

            T gameData = AssetDatabase.LoadAssetAtPath<T>(fullPath);
            EditorUtility.SetDirty(gameData);
            AssetDatabase.SaveAssets();
            return gameData; 
        } 
        private static T CreateResourceAsset<T>(string path) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
 
            AssetDatabase.CreateAsset(asset, path); 
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
            return asset;
        } 
        private static void PrepareFolders(string folderPath)
        {
            if (folderPath.Contains("Assets/") == false)
                folderPath = "Assets/" + folderPath;

            string[] paths = folderPath.TrimEnd('/').Split('/');
            for (int i = 1; i < paths.Length; i++)
            {
                string path = CombinePath(paths, i);
                string parentFolder = CombinePath(paths, i - 1);
                string targetFolder = paths[i];
                if (System.IO.Directory.Exists(path) == false)
                    AssetDatabase.CreateFolder(parentFolder, targetFolder);
            }
        }
#endif
        private static string CombinePath(string[] paths, int index)
        {
            string path = "";
            for (int i = 0; i <= index; i++)
            {
                path += paths[i];
                path += (i == index) ? "" : "/";
            }
            return path;
        }
    }
}