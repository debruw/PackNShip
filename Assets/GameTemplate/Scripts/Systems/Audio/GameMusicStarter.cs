using UnityEngine;
using VContainer;

namespace GameTemplate.Systems.Audio
{
    /// <summary>
    /// Simple class to play game theme on scene load
    /// </summary>
    public class GameMusicStarter : MonoBehaviour
    {
        // set whether theme should restart if already playing
        
        SoundService _soundService;

        [Inject]
        public void Construct(SoundService soundService)
        {
            _soundService = soundService;
            _soundService.StartGameThemeMusic(0);
        }
    }
}