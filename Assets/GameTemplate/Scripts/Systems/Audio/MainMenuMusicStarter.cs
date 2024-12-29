using UnityEngine;
using VContainer;

namespace GameTemplate.Systems.Audio
{
    /// <summary>
    /// Simple class to play game theme on scene load
    /// </summary>
    public class MainMenuMusicStarter : MonoBehaviour
    {
        // set whether theme should restart if already playing
        [SerializeField]
        bool m_Restart;
        
        SoundService _soundService;

        [Inject]
        public void Construct(SoundService soundService)
        {
            _soundService = soundService;
            _soundService.StartMenuThemeMusic(m_Restart);
        }
    }
}
