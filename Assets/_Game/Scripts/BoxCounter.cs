using System;
using TMPro;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class BoxCounter : MonoBehaviour
    {
        private TextMeshProUGUI counterText;

        int _deliveredBoxCount = 0;

        private void Start()
        {
            counterText = GetComponentInChildren<TextMeshProUGUI>();
            counterText.text = "<mspace=55>" + _deliveredBoxCount.ToString("000") + "</mspace>";

            BoxDrag.OnBoxDelivered += OnBoxDelivered;
        }

        private void OnDestroy()
        {
            BoxDrag.OnBoxDelivered -= OnBoxDelivered;
        }

        private void OnBoxDelivered()
        {
            _deliveredBoxCount++;
            counterText.text = "<mspace=55>" + _deliveredBoxCount.ToString("000") + "</mspace>";
        }
    }
}