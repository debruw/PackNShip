using System;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class BoxCounter : MonoBehaviour
    {
        private TextMeshProUGUI counterText;

        private Statistics _statistics;
        
        [Inject]
        public void Construct(Statistics Statistics)
        {
            _statistics = Statistics;
        }

        private void Start()
        {
            counterText = GetComponentInChildren<TextMeshProUGUI>();
            counterText.text = "<mspace=55>" + _statistics.DeliveredBoxCount.ToString("000") + "</mspace>";

            Box.OnBoxDelivered += OnBoxDelivered;
        }

        private void OnDestroy()
        {
            Box.OnBoxDelivered -= OnBoxDelivered;
        }

        private void OnBoxDelivered(BoxStatistic boxStatistic, Transform boxTransform)
        {
            counterText.text = "<mspace=55>" + _statistics.DeliveredBoxCount.ToString("000") + "</mspace>";
        }
    }
}