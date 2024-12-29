using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameTemplate.Systems.Currencies
{
    public class CurrencyService : ICurrencyService
    {
        private CurrencyData _CurrencyData;
        public static Action<int, int> OnCurrencyChanged;

        public enum CurrencyType
        {
            Money,
        }

        [Inject]
        private void Construct(CurrencyData data)
        {
            Debug.Log("Construct CurrencyService");
            _CurrencyData = data;

            for (int i = 0; i < _CurrencyData.currencies.Count; i++)
            {
                _CurrencyData.currencies[i].Initialize(i);
            }
        }

        public void EarnCurrency(EventArgs eventArgs)
        {
            var currencyValue = eventArgs as CurrencyArgs;
            _CurrencyData.currencies[currencyValue.currencyId].Earn(currencyValue.changeAmount);
            OnCurrencyChanged?.Invoke(currencyValue.currencyId, _CurrencyData.currencies[currencyValue.currencyId].GetAmountFromSave());
        }

        public List<Currency> GetCurrencies()
        {
            return _CurrencyData.currencies;
        }

        public void SpendCurrency(EventArgs eventArgs)
        {
            var currencyValue = eventArgs as CurrencyArgs;
            _CurrencyData.currencies[currencyValue.currencyId].Spend(currencyValue.changeAmount);
            OnCurrencyChanged?.Invoke(currencyValue.currencyId, _CurrencyData.currencies[currencyValue.currencyId].GetAmountFromSave());
        }

        public int GetCurrencyValue(CurrencyType currencyType)
        {
            return _CurrencyData.currencies[(int)currencyType].GetAmountFromSave();
        }
    }
}