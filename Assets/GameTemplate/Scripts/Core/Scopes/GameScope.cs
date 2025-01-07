using _Game.Scripts.Currency;
using AssetKits.ParticleImage;
using Cysharp.Threading.Tasks;
using GameTemplate._Game;
using GameTemplate._Game.Scripts;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate._Game.Scripts.Views;
using GameTemplate.Systems.Audio;
using GameTemplate.Systems.Currencies;
using GameTemplate.Systems.Level;
using GameTemplate.Systems.Scene;
using GameTemplate.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using SceneLoadData = GameTemplate.Systems.Scene.SceneLoadData;

namespace GameTemplate.Core.Scopes
{
    public class GameScope : GameStateScope
    {
        public override bool Persists => false;
        public override GameState ActiveState => GameState.Game;

        [SerializeField] private Transform _levelPrefabParent;
        [SerializeField] private UIGameCanvas _uiGameCanvas;
        [SerializeField] private EarningsUI _earningsUI;
        [SerializeField] private ParticleImage _winParticleImage;

        // Wait time constants for switching to post game after the game is won or lost
        private const float k_WinDelay = 2.0f;
        private const float k_LoseDelay = 2.0f;

        [Inject] PersistentGameState m_PersistentGameState;
        [Inject] LevelService _levelService;
        [Inject] ICurrencyService _currencyManager;
        [Inject] ISceneService _SceneService;
        [Inject] SoundService _soundService;


        protected override void Start()
        {
            base.Start();

            m_PersistentGameState.Reset();
            //Do some things here
            _levelService.SpawnLevel(_levelPrefabParent);

            //_uiGameCanvas.Initialize(_levelService.UILevelId);

            LevelPrefab.OnGameFinished += OnGameFinished;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponentInHierarchy<InventoryController>();
            builder.RegisterComponentInHierarchy<Statistics>();
            builder.RegisterComponentInHierarchy<RankSystem>();

            builder.RegisterComponentInHierarchy<BoxSpawner>();
            builder.RegisterComponentInHierarchy<BasketSpawner>();
            builder.RegisterComponentInHierarchy<TapeSpawner>();
            builder.RegisterComponentInHierarchy<LabelSpawner>();

            builder.RegisterComponentInHierarchy<DayText>();
            builder.RegisterComponentInHierarchy<Timer>();
            builder.RegisterComponentInHierarchy<CurrencyUIGame>();
            
            builder.RegisterComponentInHierarchy<UIGameCanvas>();
            builder.RegisterComponentInHierarchy<LevelEndView>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LevelPrefab.OnGameFinished -= OnGameFinished;
        }

        public void OnGameFinished(bool isWin)
        {
            // start the coroutine
            _ = CoroGameOver(isWin ? k_WinDelay : k_LoseDelay, isWin);
        }

        async UniTaskVoid CoroGameOver(float wait, bool gameWon)
        {
            m_PersistentGameState.SetWinState(gameWon ? WinState.Win : WinState.Loss);
            if (gameWon) _winParticleImage.Play();

            //TODO change this game to game
            // wait for game animations to finish
            await UniTask.Delay((int)(wait * 1000)); // waits for wait*1 second

            //win or lose canvas should open
            CurrencyArgs args = _earningsUI.SetEarnings();
            _currencyManager.EarnCurrency(args);
            _uiGameCanvas.GameFinished(m_PersistentGameState.WinState);

            if (m_PersistentGameState.WinState == WinState.Win)
            {
                _soundService.PlayWinSound();
                //_levelService.SetNextLevel();
            }
            else
            {
                _soundService.PlayLoseSound();
            }
        }

        public void NextButtonClick()
        {
            /*if (_levelService.LevelId < 2)
            {*/
                _SceneService.LoadScene(new SceneLoadData
                {
                    sceneName = "Game",
                    unloadCurrent = true,
                    activateLoadingCanvas = true,
                    setActiveScene = false
                });
            /*}
            else
            {
                _SceneService.LoadScene(new SceneLoadData
                {
                    sceneName = "MainMenu",
                    unloadCurrent = true,
                    activateLoadingCanvas = true,
                    setActiveScene = false
                });
            }*/
        }

        public void RetryButtonClick()
        {
            _SceneService.LoadScene(new SceneLoadData
            {
                sceneName = "Game",
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = false
            });
        }
    }
}