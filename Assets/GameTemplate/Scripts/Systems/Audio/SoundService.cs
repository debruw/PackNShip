using System;
using DG.Tweening;
using GameTemplate.Gameplay.UI;
using GameTemplate.Utils;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace GameTemplate.Systems.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundService
    {
        //use for music and theme sounds
        AudioSource _MusicSource;
        //use for effect sounds
        AudioSource _EffectSource;
        
        AudioData _audioData;
        private Transform _Holder;
        float _musicVolume;

        [Inject]
        public void Construct(AudioData audioData)
        {
            Debug.Log("Construct SoundService");
            _audioData = audioData;
            
            if (_MusicSource == null)
            {
                var clone = Object.Instantiate(_audioData.audioObject);
                clone.name = "Music";
                _MusicSource = clone.GetComponent<AudioSource>();
                Object.DontDestroyOnLoad(_MusicSource.gameObject);
            }
            
            if (_EffectSource == null)
            {
                var clone = Object.Instantiate(_audioData.audioObject);
                clone.name = "Effects";
                _EffectSource = clone.GetComponent<AudioSource>();
                Object.DontDestroyOnLoad(_EffectSource.gameObject);
            }
            
            _musicVolume = _MusicSource.volume;
            UISettingsPanel.OnMusicStateChanged += OnMusicStateChanged;
        }

        private void OnMusicStateChanged(bool state)
        {
            if (state)
            {
                StartMenuThemeMusic(false);
            }
            else
            {
                StopThemeMusic();
            }
        }

        public void StartMenuThemeMusic(bool restart)
        {
            PlayTrack(_audioData.GetAudio(AudioID.MenuMusic), true, restart);
        }
        public void StartGameThemeMusic(int orderId)
        {
            AudioClip firstClip = _audioData.GetMusicPlayerMusics(orderId);
            PlayTrack(firstClip, true, true);
        }

        public void ChangeGameThemeState(int orderId)
        {
            if (_MusicSource.isPlaying)
            {
                StopThemeMusic();
            }
            else
            {
                StartGameThemeMusic(orderId);
            }
        }
        
        public void StopThemeMusic()
        {
            StopTrack();
        }

        public void PlayWinSound()
        {
            PlaySound(_audioData.GetAudio(AudioID.Win));
        }

        public void PlayLoseSound()
        {
            PlaySound(_audioData.GetAudio(AudioID.Lose));
        }

        private void PlaySound(AudioClip clip)
        {
            if (!UserPrefs.GetSoundState()) return;
            
            if (_EffectSource == null)
            {
                Debug.LogError("Effect source is null!");
            }

            _EffectSource.clip = clip;
            _EffectSource.Play();
        }

        private void PlayTrack(AudioClip clip, bool looping, bool restart)
        {
            if (!UserPrefs.GetMusicState()) return;
            
            if (_MusicSource == null)
            {
                Debug.LogError("Music source is null!");
            }
            
            if (_MusicSource.isPlaying)
            {
                // if we dont want to restart the clip, do nothing if it is playing
                if (!restart && _MusicSource.clip == clip)
                {
                    return;
                }

                _MusicSource.Stop();
            }
            Debug.Log("play track");

            _MusicSource.volume = _musicVolume;
            _MusicSource.clip = clip;
            _MusicSource.loop = looping;
            _MusicSource.time = 0;
            _MusicSource.Play();
        }

        private void StopTrack()
        {
            _MusicSource.DOFade(0, 1).OnComplete(() =>
            {
                _MusicSource.Stop();
                _MusicSource.volume = _musicVolume;
            });
        }
    }
}