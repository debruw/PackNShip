using UnityEngine;

namespace GameTemplate.Systems.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level data holder", order = 0)]
    public class LevelDataHolder : ScriptableObject
    {
        public LevelTypes levelType;
        
        public LevelData[] levels;
    }
}