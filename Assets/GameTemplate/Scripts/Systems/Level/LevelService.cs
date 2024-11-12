using GameTemplate.Systems.Scene;
using GameTemplate.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace GameTemplate.Systems.Level
{
    public enum LevelTypes
    {
        None,
        Scene,
        Prefab
    }

    public class LevelService
    {
        private int _levelId
        {
            get => UserPrefs.GetLevelId();
            set => UserPrefs.SetLevelId(value);
        }

        ISceneService _SceneService;
        LevelDataHolder _levelDataHolder;

        [Inject]
        public void Construct(ISceneService sceneService, LevelDataHolder levelDataHolder)
        {
            Debug.Log("construct LevelService");
            _SceneService = sceneService;
            _levelDataHolder = levelDataHolder;
        }

        public int LevelId
        {
            get => _levelId;
        }

        public int UILevelId
        {
            get => _levelId + 1;
        }

        public LevelTypes LevelType
        {
            get => _levelDataHolder.levelType;
        }

        private string lastLoadedLevelScene = "";
        private GameObject lastLoadedLevelPrefab;
        
        public void SpawnLevel(Transform levelPrefabParent)
        {
            LoadLevel(levelPrefabParent);
        }

        public void LoadLevel(Transform levelPrefabParent)
        {
            int currentId = _levelId % _levelDataHolder.levels.Length;
            LevelData currentData = _levelDataHolder.levels[currentId];
            
            if (_levelDataHolder.levelType == LevelTypes.Scene)
            {
                if (lastLoadedLevelScene != "")
                {
                    SceneManager.UnloadSceneAsync(lastLoadedLevelScene);
                }
                
                lastLoadedLevelScene = currentData.levelScene.sceneName;
                //load scene additive
                _SceneService.LoadScene(new SceneLoadData
                {
                    sceneName = currentData.levelScene.sceneName,
                    unloadCurrent = false,
                    activateLoadingCanvas = false,
                    setActiveScene = false
                });
            }
            else
            {
                lastLoadedLevelPrefab = currentData.levelPrefab;
                //instantiate scene prefab
                lastLoadedLevelPrefab = Object.Instantiate(lastLoadedLevelPrefab, levelPrefabParent);
            }
        }

        public void SetNextLevel()
        {
            _levelId++;
            UserPrefs.SetLevelId(_levelId);
        }

        public void SetPreviousLevel()
        {
            _levelId--;
            UserPrefs.SetLevelId(_levelId);
        }
    }
}