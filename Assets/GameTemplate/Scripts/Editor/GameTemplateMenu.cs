using GameTemplate.Systems.Audio;
using GameTemplate.Systems.Currencies;
using GameTemplate.Systems.Level;
using GameTemplate.Systems.Pooling;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
#endif

using UnityEngine;

namespace GameTemplate.Scripts.Editor
{
#if UNITY_EDITOR
    public class GameTemplateMenu : OdinMenuEditorWindow
    {

        [MenuItem("GameTemplate/Settings", false, 30)]
        private static void OpenWindow()
        {
            Debug.Log("GameTemplate Settings");
            GetWindow<GameTemplateMenu>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            Debug.Log("BuildMenuTree");
            var tree = new OdinMenuTree();
            tree.AddAssetAtPath("Editor Game Settings", "Assets/Resources/EditorGameSettings.asset",
                typeof(EditorGameSettings));
            tree.AddAssetAtPath("Level Data Holder", "Assets/Resources/Data/LevelData.asset", typeof(LevelDataHolder));
            tree.AddAssetAtPath("Currency Data", "Assets/Resources/Data/CurrencyData.asset", typeof(CurrencyData));
            tree.AddAssetAtPath("Pooling Data", "Assets/Resources/Data/PoolingData.asset", typeof(PoolingData));
            tree.AddAssetAtPath("Audio Data", "Assets/Resources/Data/AudioData.asset", typeof(AudioData));
            return tree;
        }
    }
#endif
}