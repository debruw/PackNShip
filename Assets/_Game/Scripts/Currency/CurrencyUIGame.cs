using System;
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
        }

        private void OnCurrencyChanged(int currencyId)
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