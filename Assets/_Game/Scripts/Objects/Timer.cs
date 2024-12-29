using System;
using _Game.Scripts.Upgrades;
using Cysharp.Threading.Tasks;
using GameTemplate.Systems.Level;
using GameTemplate.Utils;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class Timer : MonoBehaviour
    {
        public static Action OnTimesUp;

        [SerializeField] private TextMeshProUGUI txtTimer;
        [SerializeField] private Color timerTextColorForFast;

        private float _totalDurationInSeconds = 30;
        private float _timer;

        private bool timerPaused;
        private bool firstTick = true;

        
        [Inject]
        public void Contruct()
        {
            Debug.Log("Construct timer");
            
            SetTimer(UserPrefs.GetLevelDuration());
        }

        private void Start()
        {
            StartTimer();
        }

        public void SetTimer(float durationInSeconds)
        {
            _totalDurationInSeconds = durationInSeconds;
            _timer = _totalDurationInSeconds;
            UpdateTimerText();
        }

        public void StopTimer(bool isWin, bool isAllLinesFilled)
        {
            timerPaused = true;
            txtTimer.color = Color.white;
        }

        public void StartTimer()
        {
            UpdateTimerText();
            
            _ = StartTimerCor();
        }
        
        private async UniTask StartTimerCor()
        {
            while (_timer > 0)
            {
                if (!timerPaused)
                {
                    _timer -= Time.deltaTime;
                    
                    UpdateTimerText();
                }
                
                await UniTask.Yield();
            }

            //Game Finished LOSE
            txtTimer.text = "00:00";
            OnTimesUp?.Invoke();
            txtTimer.color = Color.white;
            txtTimer.transform.localScale = Vector3.one;
        }

        private void UpdateTimerText()
        {
            var minutes = Mathf.Clamp(Mathf.Floor(_timer / 60f), 0, 10);
            var seconds = _timer % 60f;
            txtTimer.text = $"{minutes:00}:{MathF.Floor(Math.Clamp(seconds, 0f, 59f)):00}";
        }
    }
}