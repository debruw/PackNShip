using System;
using TMPro;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class Counter : MonoBehaviour
    {
        private TextMeshProUGUI counterText;

        private void Start()
        {
            counterText = GetComponentInChildren<TextMeshProUGUI>();
            counterText.text = 18.ToString("000");
        }
    }
}