using System;
using System.Collections.Generic;

namespace GameTemplate.Systems.Currencies
{
    public interface ICurrencyService
    {
        void EarnCurrency(EventArgs args);
        void SpendCurrency(EventArgs args);
        List<Currency> GetCurrencies();
        int GetCurrencyValue(CurrencyService.CurrencyType currencyType);
    }
}