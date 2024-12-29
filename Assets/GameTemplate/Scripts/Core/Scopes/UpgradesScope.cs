using _Game.Scripts.Upgrades;
using GameTemplate.UI.Currency;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Core.Scopes
{
    public class UpgradesScope : GameStateScope
    {
        public override bool Persists => false;
        public override GameState ActiveState => GameState.Upgrades;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponentInHierarchy<UpgradeCanvasView>();
            builder.RegisterComponentInHierarchy<CurrecyViewSpawner>();
            builder.Register<UpgradeService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}