using System;
using GameTemplate._Game.Scripts;
using GameTemplate.Systems.Currencies;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.Currency
{
    public class CurrencyUIGame : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;
        public CurrencyService.CurrencyType currencyType;
        ICurrencyService _currencyService;

        [Inject]
        public void Contruct(ICurrencyService currencyService)
        {
            Debug.Log("Contruct CurrencyUIGame");
            _currencyService = currencyService;

            UpdateText();
            CurrencyService.OnCurrencyChanged += OnCurrencyChanged;
            Timer.OnTimesUp += OnTimesUp;
        }

        private void OnTimesUp()
        {
            CurrencyService.OnCurrencyChanged -= OnCurrencyChanged;
            Timer.OnTimesUp -= OnTimesUp;
        }

        private void OnCurrencyChanged(int currencyId, int i)
        {
            if (currencyId == (int)currencyType)
            {
                UpdateText();
            }
        }

        void UpdateText()
        {
            moneyText.text = _currencyService.GetCurrencyValue(currencyType).ToString();
        }
    }
}