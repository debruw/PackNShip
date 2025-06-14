using System;
using GameTemplate.Utils;
using UnityEngine;

namespace GameTemplate.Gameplay.UI
{
    public class UISettingsPanel : MonoBehaviour
    {
        [SerializeField] private UISwitcher.UISwitcher m_SoundToggle;

        [SerializeField] private UISwitcher.UISwitcher m_MusicToggle;
        
        public static Action<bool> OnMusicStateChanged;

        private void OnEnable()
        {
            // Note that we initialize the slider BEFORE we listen for changes (so we don't get notified of our own change!)
            m_SoundToggle.isOn = UserPrefs.GetSoundState();
            m_SoundToggle.onValueChanged.AddListener(OnSoundToggleChanged);

            // initialize music slider similarly.
            m_MusicToggle.isOn = UserPrefs.GetMusicState();
            m_MusicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        }

        private void OnDisable()
        {
            m_SoundToggle.onValueChanged.RemoveListener(OnSoundToggleChanged);
            m_MusicToggle.onValueChanged.RemoveListener(OnMusicToggleChanged);
        }

        private void OnSoundToggleChanged(bool state)
        {
            Debug.Log("Sound : " + state);
            UserPrefs.SetSoundState(state);
        }

        private void OnMusicToggleChanged(bool state)
        {
            Debug.Log("Music : " + state);
            UserPrefs.SetMusicState(state);
            OnMusicStateChanged?.Invoke(state);
        }
    }
}