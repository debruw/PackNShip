using UnityEngine.AddressableAssets;

namespace GameTemplate.Systems.Scene
{
    [System.Serializable]
    public struct SceneNameData
    {
        public AssetReference scene;
        public string         sceneName;
    }
}