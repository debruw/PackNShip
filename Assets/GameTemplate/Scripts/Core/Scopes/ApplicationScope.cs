using UnityEngine;
using GameTemplate.Systems.Audio;
using GameTemplate.Systems.Currencies;
using GameTemplate.Systems.FloatingText;
using GameTemplate.Systems.Level;
using GameTemplate.Systems.Pooling;
using GameTemplate.Systems.Scene;
using GameTemplate.UI;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Core.Scopes
{
    /// <summary>
    /// An entry point to the application, where we bind all the common dependencies to the root DI scope.
    /// </summary>
    public class ApplicationScope : LifetimeScope
    {
        public AudioData audioData;
        public CurrencyData currencyData;
        public SceneData sceneData;
        public LevelDataHolder levelData;
        public PoolingData poolingData;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(audioData);
            builder.RegisterInstance(currencyData);
            builder.RegisterInstance(sceneData);
            builder.RegisterInstance(levelData);
            builder.RegisterInstance(poolingData);

            builder.Register<CurrencyService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SoundService>(Lifetime.Singleton);
            builder.Register<LevelService>(Lifetime.Singleton);
            builder.Register<PoolingService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<ApplicationCanvas>();
            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);
            builder.Register<FloatingTextService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<LoadingScreen>();

            builder.Register<PersistentGameState>(Lifetime.Singleton);
        }

        public void Start()
        {
            Application.targetFrameRate = 60;
            //SceneManager.LoadScene("MainMenu");
        }
    }
}