using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Core.UtilitySO
{
    internal static class MemberInfoExtensions
    {
        internal static bool IsPropertyWithSetter(this MemberInfo member)
        {
            var property = member as PropertyInfo;

            if (property == null)
                return false;
            else
                return property.GetSetMethod(true) != null; 
        }
    }
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.Writable)
                return jProperty;

            jProperty.Writable = member.IsPropertyWithSetter();

            return jProperty;
        }
    }

    public static class FileManager
    {
        private static readonly string folder = "Resources";
        
        public static void ResetFiles(string filename)
        {
            string save = JsonConvert.SerializeObject(null);
            string _filename = filename + ".txt";
            SaveToDataPath(save,folder, _filename);
            LogCtrl.Debug("ResetFiles : "+_filename);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        } 
        public static void SaveToDisk(object obj, string filename)
        {
            string save = JsonConvert.SerializeObject(obj,Formatting.Indented);
            string _filename = filename + ".txt";
            SaveToDataPath(save,folder, _filename);
            LogCtrl.Debug("SaveToDisk : "+_filename);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        } 
        public static object LoadFromDisk(string fileName)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            }; 
            string _filename = fileName + ".txt";
            string getFiles = GetResourcesFiles(_filename,folder);
            var jDeserializeObject = JsonConvert.DeserializeObject(getFiles, settings);
            LogCtrl.Debug("LoadFromDisk : "+_filename);
            return jDeserializeObject;
        }  
        public static T LoadFromDisk<T>(string fileName)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            }; 
            string _filename = fileName + ".txt";
            string getFiles = GetResourcesFiles(_filename,folder);
            LogCtrl.Debug("getFiles : "+getFiles);

            T jDeserializeObject = JsonConvert.DeserializeObject<T>(getFiles,settings);
            LogCtrl.Debug("LoadFromDisk : "+_filename); 

            return jDeserializeObject;
        }  
        #region private static method
        private static string GetResourcesFiles(string fileName,string folder)
        {
            string rawJson = "";
            string _fileName = fileName;
#if UNITY_EDITOR
            rawJson = LoadFromDataPath_AsString(_fileName,folder);
#else
            rawJson = Resources.Load<TextAsset>(RemoveFilenameExtension(_fileName)).text;
#endif
            return rawJson;
        }
        public static string RemoveFilenameExtension(string str)
        {
            int index = str.LastIndexOf('.');
            if (index < 0)
                return str;
        
            return str.Remove(index);
        }
        private static string LoadFromDataPath_AsString(string filename, string folderName)
        {
            // exit if no path
            if (IsNullOrEmpty(filename))
                return null;

            // build the path
            string path = IsNullOrEmpty(folderName) ?
                Path.Combine(Application.dataPath, filename) :
                Path.Combine(Path.Combine(Application.dataPath, folderName), filename);

            // load the data
            return LoadFrom_AsString(path);
        }
        private static string LoadFrom_AsString(string path)
        {
            // exit if no path
            if (IsNullOrEmpty(path))
                return null;

            // exit if the file doesn't exist
            if (!File.Exists(path))
                return null;

            // read the file
            return File.ReadAllText(path);
        }
        private static void SaveToDataPath(string data, string folderName, string filename)
        {
            // exit if no data or no filename
            if ((IsNullOrEmpty(data)) || (IsNullOrEmpty(filename)))
                return;

            string path = IsNullOrEmpty(folderName) ?
                Path.Combine(Application.dataPath, filename) :
                Path.Combine(Path.Combine(Application.dataPath, folderName), filename);

            // save the data
            SaveTo(data, path);
        }
        private static void SaveTo(string data, string path)
        {
            // exit if no data or no filename
            if ((IsNullOrEmpty(data)) || (IsNullOrEmpty(path)))
                return;

            // create the folder if it doesn't exist
            CreateDirectoryIfNotExists(path);

            // save the data
            File.WriteAllText(path, data);
        }
        private static bool IsNullOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }
        private static void CreateDirectoryIfNotExists(string folder)
        {
            if (IsNullOrEmpty(folder))
                return;

            string path = Path.GetDirectoryName(folder);
            if (IsNullOrEmpty(path))
                return;

            if (! Directory.Exists(path))
                Directory.CreateDirectory(path);
        }  
        #endregion
    }
}
