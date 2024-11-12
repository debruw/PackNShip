using VContainer;

namespace GameTemplate.Core.Scopes
{
    /// <summary>
    /// Game Logic that runs when sitting at the MainMenu. This is likely to be "nothing", as no game has been started. But it is
    /// nonetheless important to have a game state, as the GameStateBehaviour system requires that all scenes have states.
    /// </summary>
    public class MainMenuScope : GameStateScope
    {
        #region Variables

        public override GameState ActiveState => GameState.MainMenu;

        #endregion

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}