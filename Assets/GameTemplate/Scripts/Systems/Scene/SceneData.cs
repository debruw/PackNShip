using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameTemplate.Systems.Scene
{
    [CreateAssetMenu( fileName = "SceneData", menuName = "Scriptable Objects/SceneData" )]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private SceneNameData[] sceneData;

        public string nameOfSceneToLoadOnOpening;
        public string nameOfSceneUIScene;

        public AssetReference GetSceneByName( string sceneName )
        {
            foreach ( var sceneData in sceneData )
            {
                if ( sceneData.sceneName == sceneName )
                    return sceneData.scene;
            }

            throw new NullReferenceException( $"Could not find the scene reference with name {sceneName}." );
        }

        private void OnValidate( )
        {
#if UNITY_EDITOR
            for ( int i = 0; i < sceneData.Length; i++ )
            {
                var editorAsset = sceneData[i].scene.editorAsset;
                sceneData[i].sceneName = editorAsset ? editorAsset.name : "";
            }
#endif
        }
    }
}