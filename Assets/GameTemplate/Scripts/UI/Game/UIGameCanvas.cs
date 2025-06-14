using DG.Tweening;
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

        private LevelService _levelService;
        ISceneService _SceneService;

        [Inject]
        public void Construct(LevelService LevelService, ISceneService SceneService)
        {
            Debug.Log("Construct UIGameCanvas");
            _levelService = LevelService;
            _SceneService = SceneService;
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