using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector;



[CreateAssetMenu(fileName = "EditorGameSettings", menuName = "Scriptable Objects/EditorGameSettings")]
public class EditorGameSettings : ScriptableObject
{
    [TabGroup("Editor Settings")]
    public bool startFromGameScene;    
}
#endif