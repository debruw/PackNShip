using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using GameTemplate.Systems.Level;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class Timer : MonoBehaviour
    {
        public static Action<bool> OnTimesUp;
        public static Action<float> OnSetTimer;

        [SerializeField] private TextMeshProUGUI txtTimer;
        [SerializeField] private Color timerTextColorForFast;

        private float _totalDurationInSeconds = 30;
        private float _timer;

        private bool timerPaused;
        private bool firstTick = true;
        
        private void Awake()
        {
            OnSetTimer += SetTimer;
        }

        private void Start()
        {
            OnSetTimer?.Invoke(120);
            StartTimer();
        }

        private void OnDestroy()
        {
            OnSetTimer -= SetTimer;
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
            OnTimesUp?.Invoke(false);
            txtTimer.color = Color.white;
            txtTimer.transform.localScale = Vector3.one;
        }

        private bool coin0Lose, coin1Lose, coin2Lose;

        private void UpdateTimerText()
        {
            var minutes = Mathf.Clamp(Mathf.Floor(_timer / 60f), 0, 10);
            var seconds = _timer % 60f;
            txtTimer.text = $"{minutes:00}:{MathF.Floor(Math.Clamp(seconds, 0f, 59f)):00}";
        }
    }
}