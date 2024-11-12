using DG.Tweening;
using GameTemplate.Utils;
using UnityEngine;
using VContainer;

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

        [Inject]
        public void Construct(AudioData audioData)
        {
            Debug.Log("Construct SoundService");
            _audioData = audioData;
        }

        public void PlayThemeMusic(bool restart)
        {
            PlayTrack(_audioData.GetAudio(AudioID.Music), true, restart);
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
                var clone = Object.Instantiate(_audioData.audioObject);
                _EffectSource = clone.GetComponent<AudioSource>();
                Object.DontDestroyOnLoad(_EffectSource.gameObject);
            }

            _EffectSource.clip = clip;
            _EffectSource.Play();
        }

        private void PlayTrack(AudioClip clip, bool looping, bool restart)
        {
            if (!UserPrefs.GetMusicState()) return;
            
            if (_MusicSource == null)
            {
                var clone = Object.Instantiate(_audioData.audioObject);
                _MusicSource = clone.GetComponent<AudioSource>();
                Object.DontDestroyOnLoad(_MusicSource.gameObject);
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
                _MusicSource.volume = 1;
            });
        }
    }
}