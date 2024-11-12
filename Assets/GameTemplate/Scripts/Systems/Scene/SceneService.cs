using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace GameTemplate.Systems.Scene
{
    public class SceneService : ISceneService
    {
        public event Action OnBeforeSceneLoad = delegate { };

        public event Action OnSceneLoaded = delegate { };

        private SceneData _data;
        public SceneInstance LastLoadedScene { get; private set; }

        [Inject]
        private void Construct(SceneData data)
        {
            Debug.Log("Construct SceneService");
            _data = data;
            
            LoadScene(new SceneLoadData
            {
                sceneName = _data.nameOfSceneToLoadOnOpening,
                unloadCurrent = false,
                activateLoadingCanvas = true,
                setActiveScene = true
            });

            /*LoadScene(new SceneLoadData
            {
                sceneName = _data.nameOfSceneUIScene
            });*/
        }

        public async void LoadScene(SceneLoadData sceneLoadData)
        {
            if (sceneLoadData.activateLoadingCanvas)
            {
                OnBeforeSceneLoad?.Invoke();
            }

            if (sceneLoadData.unloadCurrent)
                await UnloadScene();

            var sceneReference = _data.GetSceneByName(sceneLoadData.sceneName);

            var result = await Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Additive);

            LastLoadedScene = result;

            if (sceneLoadData.setActiveScene)
                SceneManager.SetActiveScene(LastLoadedScene.Scene);

            if (sceneLoadData.activateLoadingCanvas)
            {
                OnSceneLoaded.Invoke();
            }
        }

        public async UniTask UnloadScene()
        {
            await Addressables.UnloadSceneAsync(LastLoadedScene);
        }
    }
}