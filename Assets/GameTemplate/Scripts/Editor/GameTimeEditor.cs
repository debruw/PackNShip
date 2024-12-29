using QuantumForgeStudio.EasyScreenshotTool;
using UnityEditor;
using UnityEngine;

namespace GameTemplate.Scripts.Editor
{
    [InitializeOnLoad]
    public class GameTimeEditorPlus
    {
        static GameTimeEditorPlus()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            GUILayout.Space(10);
            GUIStyle buttonStyle = new GUIStyle("Command");
            if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Plus"), buttonStyle, GUILayout.Width(32)))
            {
                SpeedPlus10();
            }
        }

        private static void SpeedPlus10()
        {
            Time.timeScale += 10f;
        }
    }
    
    [InitializeOnLoad]
    public class GameTimeEditor
    {
        static GameTimeEditor()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            GUILayout.Space(10);
            GUIStyle buttonStyle = new GUIStyle("Command");
            if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Minus"), buttonStyle, GUILayout.Width(32)))
            {
                Set1();
            }
        }

        private static void Set1()
        {
            Time.timeScale = 1f;
        }
    }
}