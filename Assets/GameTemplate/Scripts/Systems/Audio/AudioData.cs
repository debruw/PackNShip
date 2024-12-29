using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Systems.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/Audio data", order = 0)]
    public class AudioData : SerializedScriptableObject
    {
        public GameObject audioObject;

        [DictionaryDrawerSettings(KeyLabel = "AudioID", ValueLabel = "AudioClip")]
        public Dictionary<AudioID, AudioClip> AudioClips = new Dictionary<AudioID, AudioClip>();

        public List<AudioClip> MusicPlayerMusics = new List<AudioClip>();

        public AudioClip GetAudio(AudioID timesUp)
        {
            return AudioClips[timesUp];
        }

        public AudioClip GetMusicPlayerMusics(int orderId)
        {
            return MusicPlayerMusics[orderId];
        }
    }
}