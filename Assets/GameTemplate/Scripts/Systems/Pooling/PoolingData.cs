using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameTemplate.Systems.Pooling
{
    [CreateAssetMenu(fileName = "PoolingManager", menuName = "Scriptable Objects/Pooling")]
    public class PoolingData : ScriptableObject
    {
        public PoolObject[] poolObjects;

#if UNITY_EDITOR
        [Button("Apply Pool")]
        public void Generate()
        {
            string filePathAndName = "Assets/GameTemplate/Scripts/Systems/Pooling/PoolId.cs"; //The folder Scripts/Enums/ is expected to exist

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine("public enum PoolID");
                streamWriter.WriteLine("{");
                for (int i = 0; i < poolObjects.Length; i++)
                {
                    streamWriter.WriteLine("\t" + poolObjects[i].poolName + ",");
                }
                streamWriter.WriteLine("}");
            }
            AssetDatabase.Refresh();
        }
#endif
    }
}