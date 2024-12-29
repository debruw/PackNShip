using GameTemplate.Systems.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate._Game.Scripts
{
    public class MusicPlayer : MonoBehaviour, IStartable
    {
        private SoundService _soundService;

        private int orderId = 0;

        [Inject]
        public void Contruct(SoundService soundService)
        {
            _soundService = soundService;
        }

        public void PlayButtonClick()
        {
            _soundService.ChangeGameThemeState(orderId);
        }

        public void NextButtonClick()
        {
            orderId++;
            _soundService.StartGameThemeMusic(orderId);
        }

        public void PreviousButtonClick()
        {
            orderId--;
            _soundService.StartGameThemeMusic(orderId);
        }

        public void Start()
        {
            
        }
    }
}