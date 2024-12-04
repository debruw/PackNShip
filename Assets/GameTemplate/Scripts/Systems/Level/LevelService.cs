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
        Prefab,
        Continuous
    }

    public class LevelService
    {
        private int _levelId
        {
            get => UserPrefs.GetLevelId();
            set => UserPrefs.SetLevelId(value);
        }

        [HideInInspector]
        public LevelData CurrentLevelData
        {
            get
            {
                int currentId = _levelId % _levelDataHolder.levels.Length;
                _currentLevelData = _levelDataHolder.levels[currentId];
                return _currentLevelData;
            }
        }

        private LevelData _currentLevelData;

        ISceneService _SceneService;
        LevelDataHolder _levelDataHolder;

        [Inject]
        public void Construct(ISceneService sceneService, LevelDataHolder levelDataHolder)
        {
            Debug.Log("Construct LevelService");
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
            switch (_levelDataHolder.levelType)
            {
                case LevelTypes.Scene:
                {
                    if (lastLoadedLevelScene != "")
                    {
                        SceneManager.UnloadSceneAsync(lastLoadedLevelScene);
                    }

                    lastLoadedLevelScene = _currentLevelData.levelScene.sceneName;
                    //load scene additive
                    _SceneService.LoadScene(new SceneLoadData
                    {
                        sceneName = _currentLevelData.levelScene.sceneName,
                        unloadCurrent = false,
                        activateLoadingCanvas = false,
                        setActiveScene = false
                    });
                    break;
                }
                case LevelTypes.Prefab:
                {
                    lastLoadedLevelPrefab = _currentLevelData.levelPrefab;
                    //instantiate scene prefab
                    lastLoadedLevelPrefab = Object.Instantiate(lastLoadedLevelPrefab, levelPrefabParent);
                    break;
                }
                case LevelTypes.Continuous:
                {
                    break;
                }
            }
        }

        public void SetNextLevel()
        {
            UserPrefs.SetLevelId(_levelId + 1);
        }
    }
}