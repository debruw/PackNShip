using System.Collections.Generic;
using GameTemplate.Systems.Audio;
using GameTemplate.Systems.Currencies;
using GameTemplate.Systems.Level;
using GameTemplate.Systems.Scene;
using GameTemplate.UI;
using GameTemplate.UI.Currency;
using UnityEngine;
using VContainer;
using SceneLoadData = GameTemplate.Systems.Scene.SceneLoadData;

namespace GameTemplate.Gameplay.UI
{
    public class MenuUICanvas : MonoBehaviour
    {
        [SerializeField] private Transform currencyParent;
        [SerializeField] private GameObject CurrencyUIPrefab;
        public List<CurrencyUI> currencyPanels = new List<CurrencyUI>();

        ISceneService _SceneService;
        SoundService _soundService;
        LevelService _levelService;
        ICurrencyService _CurrencyManager;

        [Inject]
        public void Construct(ISceneService sceneLoader, SoundService soundService, LevelService levelService,
            ICurrencyService currencyManager)
        {
            Debug.Log("Construct UICanvas");
            _SceneService = sceneLoader;
            _soundService = soundService;
            _levelService = levelService;
            _CurrencyManager = currencyManager;

            GetComponentInChildren<LevelTextSetter>().SetLevelText(_levelService.UILevelId);
            
            List<Currency> currencies = _CurrencyManager.GetCurrencies();

            for (int i = 0; i < currencies.Count; i++)
            {
                currencyPanels.Add(Instantiate(CurrencyUIPrefab, currencyParent).GetComponent<CurrencyUI>());
                currencyPanels[i].transform.SetParent(currencyParent);
                currencyPanels[i].Initialize(currencies[i].currencyImage, currencies[i].currencySign,
                    currencies[i].currencyAmount, currencies[i].isBuyable);
            }
        }

        public void PlayButtonClick()
        {
            _soundService.StopThemeMusic();
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