using GameTemplate.Systems.Scene;
using GameTemplate.Utils;
using UnityEngine;

namespace GameTemplate.Systems.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level data", order = 0)]
    public class LevelData : ScriptableObject
    {
        public GameObject levelPrefab;
        public SceneNameData levelScene;
        public int LevelTime;
    }
}