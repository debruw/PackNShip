using GameTemplate.Systems.Scene;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Upgrades
{
    public class UpgradeCanvasView : MonoBehaviour
    {
        public Transform gridTransform;
        
        ISceneService _SceneService;

        [Inject]
        public void Contruct(ISceneService SceneService)
        {
            _SceneService = SceneService;
        }
        
        public void NextDayButtonClick()
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