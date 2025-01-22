using DG.Tweening;
using GameTemplate._Game.Scripts;
using GameTemplate._Game.Scripts.Views;
using GameTemplate.Core.Scopes;
using GameTemplate.Systems.Level;
using GameTemplate.Systems.Scene;
using GameTemplate.Utils;
using UnityEngine;
using VContainer;

namespace GameTemplate.UI
{
    public class UIGameCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject TopPanel, WinPanel, LosePanel;
        [SerializeField]
        private GameObject TutorialPanel;

        [SerializeField] private LevelEndView levelEndView;

        private LevelService _levelService;
        ISceneService _SceneService;

        [Inject]
        public void Construct(LevelService LevelService, ISceneService SceneService)
        {
            Debug.Log("Construct UIGameCanvas");
            _levelService = LevelService;
            _SceneService = SceneService;

            if (UserPrefs.IsFirstPlay())
            {
                Instantiate(TutorialPanel, transform);
            }
        }

        private void Start()
        {
            Timer.OnTimesUp += OnTimesUp;
        }

        private void OnDestroy()
        {
            Timer.OnTimesUp -= OnTimesUp;
        }
        
        private void OnTimesUp()
        {
            levelEndView.InitializeAndShow();
        }

        public void Initialize(int UIlevelID)
        {
            LevelTextSetter[] levelTextSetters = GetComponentsInChildren<LevelTextSetter>();
            foreach (var levelTextSetter in levelTextSetters)
            {
                levelTextSetter.SetLevelText(UIlevelID);
            }

            if (UIlevelID == 1)
            {
                TopPanel.SetActive(false);
            }
        }
        
        public void GameFinished(WinState gameWon)
        {
            if (gameWon == WinState.Win)
            {
                OpenPanel(WinPanel.GetComponent<CanvasGroup>());
            }
            else
            {
                OpenPanel(LosePanel.GetComponent<CanvasGroup>());
            }
        }

        void OpenPanel(CanvasGroup group)
        {
            group.DOFade(1, 1);
            group.interactable = true;
            group.blocksRaycasts = true;
        }

        public void EndTheDayClick()
        {
            _levelService.SetNextLevel();
            _SceneService.LoadScene(new SceneLoadData
            {
                sceneName = "Upgrades",
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = true
            });
        }
    }
}