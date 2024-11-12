using System;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace GameTemplate.Systems.Scene
{
    public interface ISceneService
    {
        event Action  OnBeforeSceneLoad;
        event Action  OnSceneLoaded;
        SceneInstance LastLoadedScene { get; }
        void LoadScene( SceneLoadData sceneLoadData );
        UniTask UnloadScene( );
    }
}