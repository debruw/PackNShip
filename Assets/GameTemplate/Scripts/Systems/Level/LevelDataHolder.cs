using System;
using GameTemplate.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Systems.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level data holder", order = 0)]
    public class LevelDataHolder : ScriptableObject
    {
        public LevelTypes levelType;

        public LevelData[] levels;
#if UNITY_EDITOR
        [ReadOnly] public int _level;

        private void OnValidate()
        {
            _level = UserPrefs.GetLevelId();
        }

        [Button]
        public void IncreaseLevel()
        {
            int level = UserPrefs.GetLevelId() + 1;
            UserPrefs.SetLevelId(level);
            _level = level;
        }
        
        [Button]
        public void DecreaseLevel()
        {
            int level = UserPrefs.GetLevelId() - 1;
            UserPrefs.SetLevelId(level);
            _level = level;
        }
#endif
    }
}