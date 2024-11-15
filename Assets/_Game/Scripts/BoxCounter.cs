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
            counterText.text = _deliveredBoxCount.ToString("000");
        }
    }
}