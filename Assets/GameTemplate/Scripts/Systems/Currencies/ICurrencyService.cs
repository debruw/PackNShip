using System;
using System.Collections.Generic;

namespace GameTemplate.Systems.Currencies
{
    public interface ICurrencyService
    {
        void EarnCurrency(EventArgs args);
        List<Currency> GetCurrencies();
    }
}