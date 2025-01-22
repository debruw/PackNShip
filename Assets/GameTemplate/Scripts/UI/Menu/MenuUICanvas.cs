using System.Collections.Generic;
using GameTemplate.Systems.Audio;
using GameTemplate.Systems.Currencies;
using GameTemplate.Systems.Level;
using GameTemplate.Systems.Scene;
using GameTemplate.UI.Currency;
using GameTemplate.Utils;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using SceneLoadData = GameTemplate.Systems.Scene.SceneLoadData;

namespace GameTemplate.Gameplay.UI
{
    public class MenuUICanvas : MonoBehaviour
    {
        [SerializeField] Button ContinueButton;
        [SerializeField] GameObject ConfirmPanel;

        ISceneService _SceneService;
        SoundService _soundService;
        
        [Inject]
        public void Construct(ISceneService sceneLoader, SoundService soundService)
        {
            Debug.Log("Construct MenuUICanvas");
            _SceneService = sceneLoader;
            _soundService = soundService;

            ContinueButton.interactable = !UserPrefs.IsFirstPlay();
        }

        public void PlayButtonClick()
        {
            if (!UserPrefs.IsFirstPlay())
            {
                ConfirmPanel.SetActive(true);
                return;
            }

            //UserPrefs.SetFirstPlayFalse();
            UserPrefs.SetLevelDuration(180);

            LoadLevelScene();
        }

        public void ContinueButtonClick()
        {
            LoadLevelScene();
        }

        public void StartOverClick()
        {
            UserPrefs.DeleteAll();
            PlayButtonClick();
        }

        public void LoadLevelScene()
        {
            _SceneService.LoadScene(new SceneLoadData
            {
                sceneName = "Game",
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = true
            });
        }
    }
}